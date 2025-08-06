using System.Net;
using BookStore.Domain;

namespace BookStore.Application.CreatePublisher;

public class CreatePublisherResponse
{
    public HttpStatusCode  StatusCode { get; init; }
    public string Message { get; set; } = null!;
    public string Id { get; set; } = null!;

    public CreatePublisherResponse(string id)
    {
        this.Id = id;
        if (id.IsValidObjectId())
        {
            this.StatusCode = HttpStatusCode.Created;
            this.Message = "Created";
        }
        else
        {
            this.StatusCode = HttpStatusCode.BadRequest;
            this.Message = "BadRequest";
        }
    }

    public CreatePublisherResponse()
    {
        
    }
}
