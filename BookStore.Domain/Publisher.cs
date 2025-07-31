using MongoDB.Bson.Serialization.Attributes;

namespace BookStore.Domain;

public class Publisher
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
}