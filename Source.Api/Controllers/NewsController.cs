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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var news = await _repository.GetAllNews();
            var newsForReturn = _mapper.Map<IEnumerable<NewsDto>>(news);
            return Ok(newsForReturn);
        }

        [HttpGet("filter={queryFilter}")]
        public async Task<IActionResult> Get(string queryFilter)
        {   
            var news =  await _repository.GetNews(queryFilter);

            var newsForReturn = _mapper.Map<IEnumerable<NewsDto>>(news);
            return Ok(newsForReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var news = await _repository.getNewsById(id);

            var newsForReturn = _mapper.Map<NewsDto>(news);
            return Ok(newsForReturn);
        }

        [HttpPost]
        public async Task <IActionResult> AddNews(News news)
        {
            await _repository.AddNews(news);
            return Created($"/api/News/news.id",new {news.Id});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(string id)
        {
            var newsExists = await _repository.getNewsById(id);

            if (newsExists == null)
            {
                return NotFound("News not found with this ID");
            }
            await _repository.RemoveNews(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(string id, News news)
        {
            var newsExists = await _repository.getNewsById(id);

            if (newsExists == null)
            {
                return NotFound("News not found with this ID");
            }
            await _repository.UpdateNews(id, news);
            return Ok();
        }

        [HttpOptions]
        public IActionResult GetNewsOptions()
        {
            Response.Headers.Add("Allow","GET,OPTIONS,POST,DELETE,GET/FILTER={queryFilter}");
            return Ok();
        }
    }
}