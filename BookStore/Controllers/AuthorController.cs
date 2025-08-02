using System.Net;
using BookStore.Application.GetAllPublishers;
using BookStore.Application.GetAuthorById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController(IMediator mediator) : ControllerBase
{
    private IMediator mediator = mediator;

    [HttpGet("GetAllAuthors")]
    public async Task<IActionResult> GetAllAuthors(CancellationToken cancellationToken)
    {
        var response = await this.mediator.Send(new GetAllPublishersRequest(),cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(response);
        }

        return BadRequest();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(string id, CancellationToken cancellationToken)
    {
        var request = new GetAuthorByIdRequest() { Id = id };
        var response = await this.mediator.Send(request, cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest();
    }

}