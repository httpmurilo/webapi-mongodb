using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Source.Data.Context;
using Source.Data.Helpers;
using Source.Domain.Interfaces;
using System.Linq;
using Source.Domain.Model;

namespace Source.Repository.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsContext _context;
        private readonly AuthorContext _authorContext;
        public NewsRepository(IOptions<DatabaseSettings> options)
        {
            _context = new NewsContext(options) ?? throw new ArgumentNullException(nameof(options));
            _authorContext = new AuthorContext(options) ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<IEnumerable<News>> GetAllNews()
        {
            return await _context.News.Find(_ => true).ToListAsync();
        }
        public async Task<IEnumerable<object>> GetNews(string parameter)
        {

            var query = (from news in _context.News.AsQueryable()
                         join author in _authorContext.Authors.AsQueryable() 
                         on news.AuthorId equals author.Id 
                         into resultsAuthor
                         where (news.Body.Contains(parameter) || news.Title.Contains(parameter))
                         select new
                         {
                             body = news.Body,
                             title = news.Title,
                             author = resultsAuthor,

                         });
                         
            return await Task.FromResult(query.ToList());
 
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


        public async Task<News> getNewsById(string id)
        {
            return await _context.News.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}