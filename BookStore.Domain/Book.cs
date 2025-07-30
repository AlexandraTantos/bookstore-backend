using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStore.Domain
{
  public class Book : BookDto
  {

    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
  }
}
