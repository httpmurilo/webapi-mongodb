using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Source.Domain.Model
{
    public class News
    {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      public string Id { get; set; }

      [BsonElement("Title")]
      [BsonRequired()]
      public string Title { get; set; }
      
      [BsonElement("Body")]
      [BsonRequired()]
      public string Body { get; set; }
      
      [BsonRepresentation(BsonType.ObjectId)]
      public string AuthorId { get; set;}
      
      [BsonDateTimeOptions]
      public DateTime RegisteredIn { get; set; } = DateTime.Now;
    }
}