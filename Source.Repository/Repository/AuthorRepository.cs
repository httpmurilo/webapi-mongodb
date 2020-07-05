using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Source.Data.Context;
using Source.Data.Helpers;
using Source.Domain.Interfaces;
using Source.Domain.Model;

namespace Source.Repository.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AuthorContext _context;
        public AuthorRepository(IOptions<DatabaseSettings> options)
        {
            _context = new AuthorContext(options) ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _context.Authors.Find(_ => true).ToListAsync();
        }

        public async Task AddAuthor(Author author)
        {
            await _context.Authors.InsertOneAsync(author);
        }

    }
}