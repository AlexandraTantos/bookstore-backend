using System.Net;

namespace BookStore.Application.InsertUser;

public class InsertUserResponse
{
    public string Id { get; set; } = null!;
    public string Message { get; set; } = null!;
    public HttpStatusCode StatusCode { get; init; }
  
    public InsertUserResponse() { }

    public InsertUserResponse(string id)
    {
        Id = id;
        Message = "User created successfully";
        StatusCode = HttpStatusCode.Created;
    }
}