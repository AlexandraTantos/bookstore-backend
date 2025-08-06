using System.Net;

namespace BookStore.Application.DeleteBook;

public class DeleteBookResponse
{
    public HttpStatusCode StatusCode {get; init;}
    public string Message {get; set;} = null!;
}