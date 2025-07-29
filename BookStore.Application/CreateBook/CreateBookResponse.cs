using BookStore.Domain;
using System.Net;

namespace BookStore.Application.CreateBook
{
  public class CreateBookResponse
  {
    public HttpStatusCode StatusCode {  get; set; } 

    public string Message { get; set; } = string.Empty;

    public string Id { get; set; }

    public CreateBookResponse(string Id)
    {
      this.Id = Id;
      if (Id != null && Id.IsValidObjectId())
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
