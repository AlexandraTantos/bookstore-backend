using MongoDB.Bson.Serialization.Attributes;

namespace BookStore.Domain;

public class User 
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } 
    public Role Role { get; set; }
}