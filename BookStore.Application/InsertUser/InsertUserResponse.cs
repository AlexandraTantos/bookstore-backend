using System.Net;

namespace BookStore.Application.InsertUser;

public class InsertUserResponse
{
    public string Id { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
  
    public InsertUserResponse() { }

    public InsertUserResponse(string id)
    {
        Id = id;
        Message = "User created successfully";
        StatusCode = HttpStatusCode.Created;
    }
}