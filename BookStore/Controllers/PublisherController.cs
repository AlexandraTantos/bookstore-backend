using BookStore.Application.GetAllPublishers;
using BookStore.Application.GetPublisherById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;
[ApiController]
[Route("[controller]")]
public class PublisherController(IMediator mediator) : ControllerBase
{
    private IMediator mediator = mediator;

    [HttpGet("GetAllPublishers")]
    public async Task<IActionResult> GetAllPublishers(CancellationToken cancellationToken)
    {
        var response = await this.mediator.Send(new GetAllPublishersRequest(), cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPublisherById(string id, CancellationToken cancellationToken)
    {
        var request = new GetPublisherByIdRequest { Id = id };
        var response = await this.mediator.Send(request, cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest();
    }
    
}