using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Source.Domain.Model
{
    public class Author
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [BsonRequired()]
        public string Name { get; set;}
        
    }
}