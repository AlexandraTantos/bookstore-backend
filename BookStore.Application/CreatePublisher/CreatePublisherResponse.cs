using System.Net;
using BookStore.Domain;

namespace BookStore.Application.CreatePublisher;

public class CreatePublisherResponse
{
    public HttpStatusCode  StatusCode { get; set; }
    public string Message { get; set; }
    public string Id { get; set; }

    public CreatePublisherResponse(string id)
    {
        this.Id = id;
        if (Id != null && Id.IsValidObjectId())
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
