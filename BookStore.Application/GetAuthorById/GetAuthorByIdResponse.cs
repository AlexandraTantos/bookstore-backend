using System.Net;
using BookStore.Domain;

namespace BookStore.Application.GetAuthorById;

public class GetAuthorByIdResponse
{
    public AuthorDto Author { get; set; } = new();
    public HttpStatusCode StatusCode { get; init; }
    public string Message { get; set; }
}