using System.Net;

namespace BookStore.Application.GetBookById;

public class GetBookByIdResponse
{
    public BookDto Book { get; set; } = new();
    public HttpStatusCode StatusCode { get; init; }
    public string Message { get; set; }
}