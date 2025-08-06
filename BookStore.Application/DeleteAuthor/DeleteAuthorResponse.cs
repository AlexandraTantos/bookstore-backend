using System.Net;

namespace BookStore.Application.DeleteAuthor;

public class DeleteAuthorResponse
{
    public HttpStatusCode StatusCode { get; init; }
    public string Message { get; set; } = null!;
}