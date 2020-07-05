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
    }
}