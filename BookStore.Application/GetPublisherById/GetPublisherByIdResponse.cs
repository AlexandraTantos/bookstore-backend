using System.Net;
using BookStore.Domain;

namespace BookStore.Application.GetPublisherById;

public class GetPublisherByIdResponse
{
    public PublisherDto Publisher { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}