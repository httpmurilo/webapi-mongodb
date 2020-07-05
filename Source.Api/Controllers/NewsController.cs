using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Source.Api.Dto;
using Source.Domain.Interfaces;
using Source.Domain.Model;

namespace Source.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _repository;
        private readonly IMapper _mapper;
        public NewsController(INewsRepository repository, IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }
        
        [HttpGet("getByParameter/{parameter}")]
        public async Task<IActionResult> Get(string parameter)
        {   
            var news =  await _repository.GetNews(parameter);
            var newsForReturn = _mapper.Map<IEnumerable<NewsDto>>(news);
            return Ok(newsForReturn);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var news = await _repository.GetNewsForId(id);
            var newsForReturn = _mapper.Map<NewsDto>(news);
            return Ok(newsForReturn);
        }

        [HttpPost]
        public async Task <IActionResult> AddNews(News news)
        {
             await _repository.AddNews(news);
             return Created($"/api/Author/","");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(string id)
        {
            var newsExists = await _repository.GetNewsForId(id);
            if(newsExists == null)
            {
                return NotFound("News not found with this ID");
            }
            await _repository.RemoveNews(id);
            return Ok();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateNews(string id, News news)
        {
            var newsExists = await _repository.GetNewsForId(id);
            if(newsExists == null)
            {
                return NotFound("News not found with this ID");
            }
            await _repository.UpdateNews(id, news);
            return Ok();
        }
    }
}