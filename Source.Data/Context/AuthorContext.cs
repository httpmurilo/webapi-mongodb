using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Source.Data.Helpers;
using Source.Domain.Model;

namespace Source.Data.Context
{
    public class AuthorContext
    {
        private readonly IMongoDatabase _base;

        public AuthorContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.Url);
            if (client != null)
            {
                _base = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<Author> Authors
        {
            get
            {
                return _base.GetCollection<Author>("Authors");
            }
        }
    }
}