using System.Net;
using BookStore.Domain;

namespace BookStore.Application.CreateAuthor;

public class CreateAuthorResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Id { get; set; }

    public CreateAuthorResponse(string Id)
    {
        this.Id = Id;
        if (Id != null && Id.IsValidObjectId())
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