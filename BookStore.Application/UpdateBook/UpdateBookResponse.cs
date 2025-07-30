using System.Net;

namespace BookStore.Application.UpdateBook;

public class UpdateBookResponse
{
    public HttpStatusCode  StatusCode{ get; set; } = new();
    public string? Message { get; set; }
    public BookDto? UpdatedBook { get; set; }
}