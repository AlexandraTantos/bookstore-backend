using System.Net;

namespace BookStore.Application.DeleteBook;

public class DeleteBookResponse
{
    public HttpStatusCode StatusCode {get; set;}
    public string Message {get; set;}
}