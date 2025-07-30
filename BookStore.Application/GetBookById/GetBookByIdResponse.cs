using System.Net;
using BookStore.Domain;

namespace BookStore.Application.GetBookById;

public class GetBookByIdResponse
{
    public BookDto Book { get; set; } = new();
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}