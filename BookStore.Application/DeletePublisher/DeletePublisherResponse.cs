using System.Net;

namespace BookStore.Application.DeletePublisher;

public class DeletePublisherResponse
{
    public HttpStatusCode StatusCode {get; init;}
    public string Message {get; set;} = null!;
}