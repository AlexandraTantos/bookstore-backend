using BookStore.Domain;
using System.Net;

namespace BookStore.Application.CreateBook
{
  public class CreateBookResponse
  {
    public HttpStatusCode StatusCode {  get; init; } 

    public string Message { get; set; } = string.Empty;

    public string Id { get; set; } = null!;

    public CreateBookResponse(string id)
    {
      this.Id = id;
      if (id.IsValidObjectId())
      {
        this.StatusCode = HttpStatusCode.Created;
        this.Message = "Created";
      }
      else
      {
        this.StatusCode = HttpStatusCode.BadRequest;
        this.Message = "BadRequest";
      }
    }
    public CreateBookResponse()
    {
    
    }
  }
}
