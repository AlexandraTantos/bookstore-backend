using System.Net;
using BookStore.Domain;

namespace BookStore.Application.CreateAuthor;

public class CreateAuthorResponse
{
    public HttpStatusCode StatusCode { get; init; }
    public string Message { get; set; } = string.Empty;
    public string Id { get; set; } = null!;

    public CreateAuthorResponse(string id)
    {
        this.Id = id;
        if (id.IsValidObjectId())
        {
            this.StatusCode =  HttpStatusCode.Created;
            this.Message = "Author Created";
        }
        else
        {
            this.StatusCode = HttpStatusCode.BadRequest;
            this.Message = "Invalid Id";
        }
    }

    public CreateAuthorResponse()
    {
        
    }
}