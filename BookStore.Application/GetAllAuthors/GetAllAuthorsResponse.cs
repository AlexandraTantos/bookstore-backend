using System.Net;
using BookStore.Domain;

namespace BookStore.Application.GetAllAuthors;

public class GetAllAuthorsResponse
{
    public List<AuthorDto> Authors { get; set; }
    public HttpStatusCode  StatusCode { get; set; }
    public string Message { get; set; }
}