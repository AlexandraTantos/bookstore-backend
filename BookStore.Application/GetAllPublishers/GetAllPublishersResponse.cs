using System.Net;
using BookStore.Domain;

namespace BookStore.Application.GetAllPublishers;

public class GetAllPublishersResponse
{
    public List<PublisherDto>  Publishers { get; set; } = new();
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}