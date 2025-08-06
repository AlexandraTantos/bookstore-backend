using System.Net;
using BookStore.Domain;

namespace BookStore.Application.UpdatePublisher;

public class UpdatePublisherResponse
{
    public PublisherDto UpdatedPublisher {get; set;}
    public HttpStatusCode StatusCode {get; init;}
    public string Message {get; set;} = null!;
}