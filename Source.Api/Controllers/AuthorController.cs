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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;
        public AuthorController(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var authors = await _repository.GetAllAuthors();
            var authorForReturn = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(authorForReturn);
        }

        [HttpPost]
        public async Task <IActionResult> AddAuthor(Author author)
        {
             await _repository.AddAuthor(author);
             return Created($"/api/Author/id","");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(string id)
        {
            var authorExists = await _repository.GetAuthorById(id);

            if(authorExists == null)
            {
                return NotFound("Author not found with this ID");
            }
            await _repository.RemoveAuthor(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(string id, Author author)
        {
            var authorExists = await _repository.GetAuthorById(id);
            
            if(authorExists == null)
            {
                return NotFound("Author not found with this ID");
            }
            await _repository.UpdateAuthor(id, author);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var author = await _repository.GetAuthorById(id);

            var authorForReturn = _mapper.Map<AuthorDto>(author);
            return Ok(authorForReturn);
        }
    }
}