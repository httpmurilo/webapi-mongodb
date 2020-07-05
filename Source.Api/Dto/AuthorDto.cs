using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Source.Api.Dto
{
    public class AuthorDto
    {
        public  string id { get; set; }
        public string Name { get; set; }
    }
}