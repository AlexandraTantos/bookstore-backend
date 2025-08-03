using System.Net;
using BookStore.Domain;

namespace BookStore.Application.UpdateAuthor;

public class UpdateAuthorResponse
{
    public HttpStatusCode  StatusCode{ get; set; } = new();
    public string? Message { get; set; }
    public AuthorDto? UpdatedAuthor { get; set; }
}