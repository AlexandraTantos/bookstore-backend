using System.Net;
using BookStore.Domain;

namespace BookStore.Application.GetAllBooks;

public class GetAllBooksResponse
{
    public List<BookDto> Books { get; set; } = new();
    public HttpStatusCode StatusCode {  get; set; } 
    public string Message { get; set; } = string.Empty;

}