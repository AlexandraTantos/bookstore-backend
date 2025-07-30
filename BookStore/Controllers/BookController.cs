using BookStore.Application.CreateBook;
using BookStore.Application.GetAllBooks;
using BookStore.Application.GetBookById;
using BookStore.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class BookController(IMediator mediator) : ControllerBase
  {
    private IMediator mediator = mediator;

    [HttpPost("InsertBook")]
    public async Task<IActionResult> InsertBook([FromBody] CreateBookRequest request,
      CancellationToken cancellationToken)
    {
      var response = await this.mediator.Send(request, cancellationToken);
      if (response.StatusCode == System.Net.HttpStatusCode.OK ||
          response.StatusCode == System.Net.HttpStatusCode.Created)
      {
        return Ok(response);
      }

      return BadRequest(response);
    }

    [HttpGet("GetAllBooks")]
    public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken)
    {
      var response = await this.mediator.Send(new GetAllBooksRequest(), cancellationToken);
      if (response.StatusCode == System.Net.HttpStatusCode.OK)
      {
        return Ok(response);
      }

      return BadRequest(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(string id, CancellationToken cancellationToken)
    {
      var request = new GetBookByIdRequest { Id = id };
      var response = await this.mediator.Send(request, cancellationToken);
      if (response.StatusCode == System.Net.HttpStatusCode.OK)
      {
        return Ok(response);
      }
      return BadRequest(response);
    }
  }
}
