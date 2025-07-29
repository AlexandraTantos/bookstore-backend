using BookStore.Application.CreateBook;
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
    public async Task<IActionResult> InsertBook([FromBody] CreateBookRequest request,CancellationToken cancellationToken)
    {
      var response = await this.mediator.Send(request, cancellationToken);
      if(response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
      {
        return Ok(response);
      }
      return BadRequest(response);
    }
  }
}
