using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStore.Domain
{
  public class BookDto
  {
    [BsonRepresentation(BsonType.ObjectId)]
    public string AuthorId { get; set; }

    public string Title { get; set; }

    public DateTime YearOfPublication { get; set; }

    public List<string> Genres { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string PublisherId { get; set; }
  }
}
