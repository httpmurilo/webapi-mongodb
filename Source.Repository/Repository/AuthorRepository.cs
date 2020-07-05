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

        public async Task<bool> RemoveAuthor(string id)
        {
            DeleteResult actionResult = await _context.Authors.DeleteOneAsync(Builders<Author>.Filter.Eq("Id",id));
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateAuthor(string id, Author author)
        {
            ReplaceOneResult actionResult = await _context.Authors.ReplaceOneAsync(x => x.Id.Equals(id),
                                                author,
                                                new ReplaceOptions { IsUpsert  = true});

            return actionResult.IsAcknowledged && actionResult.ModifiedCount >0;
        }

        public async Task<Author> GetAuthorFromId(string id)
        {
             return await _context.Authors.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}