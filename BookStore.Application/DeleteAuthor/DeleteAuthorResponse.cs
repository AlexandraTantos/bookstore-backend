using System.Net;

namespace BookStore.Application.DeleteAuthor;

public class DeleteAuthorResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}