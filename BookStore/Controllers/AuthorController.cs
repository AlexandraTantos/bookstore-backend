using System.Net;
using BookStore.Application.CreateAuthor;
using BookStore.Application.DeleteAuthor;
using BookStore.Application.GetAllAuthors;
using BookStore.Application.GetAllPublishers;
using BookStore.Application.GetAuthorById;
using BookStore.Application.UpdateAuthor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetAllAuthors")]
    public async Task<IActionResult> GetAllAuthors([FromQuery] GetAllAuthorsRequest request,CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(string id, CancellationToken cancellationToken)
    {
        var request = new GetAuthorByIdRequest() { Id = id };
        var response = await mediator.Send(request, cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest();
    }

    [HttpPost("Insert Author")]
    public async Task<IActionResult> InsertAuthor([FromBody] CreateAuthorRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(string id, CancellationToken cancellationToken)
    {
        var request = new DeleteAuthorRequest { Id = id };
        var response = await mediator.Send(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(string id, [FromBody] UpdateAuthorRequest request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        var response = await mediator.Send(request, cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}