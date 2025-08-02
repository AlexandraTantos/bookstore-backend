using System.Net;

namespace BookStore.Application.DeletePublisher;

public class DeletePublisherResponse
{
    public HttpStatusCode StatusCode {get; set;}
    public string Message {get; set;}
}