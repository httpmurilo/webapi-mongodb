using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Source.Data.Helpers;
using Source.Domain.Model;

namespace Source.Data.Context
{
    public class NewsContext
    {
        private readonly IMongoDatabase _base;

        public NewsContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.Url);
            if (client != null)
            {
                _base = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<News> News
        {
            get
            {
                return _base.GetCollection<News>("News");
            }
        }
    }
}