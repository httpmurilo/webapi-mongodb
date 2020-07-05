using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Source.Data.Context;
using Source.Data.Helpers;
using Source.Domain.Interfaces;
using Source.Domain.Model;

namespace Source.Repository.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsContext _context;
        public NewsRepository(IOptions<DatabaseSettings> options)
        {
            _context = new NewsContext(options) ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<IEnumerable<News>> GetAllNews()
        {
            return await _context.News.Find(_ => true).ToListAsync();
        }
        public async Task<IEnumerable<News>> GetNews(string parameter)
        {
            var query = _context.News.Find(x => x.Title.Contains(parameter) || x.Body.Contains(parameter));
            return await query.ToListAsync();
        }
        public async Task AddNews(News news)
        {   
            await _context.News.InsertOneAsync(news);
        }

        public async Task<bool> RemoveNews(string id)
        {
            DeleteResult actionResult = await _context.News.DeleteOneAsync(Builders<News>.Filter.Eq("Id",id));
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateNews(string id, News news)
        {
            ReplaceOneResult actionResult = await _context.News.ReplaceOneAsync(x => x.Id.Equals(id),
                                                news,
                                                new ReplaceOptions { IsUpsert  = true});

            return actionResult.IsAcknowledged && actionResult.ModifiedCount >0;
        }


        public async Task<News> GetNewsForId(string id)
        {
            return await _context.News.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}