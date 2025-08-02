using BookStore.Application.CreatePublisher;
using BookStore.Application.DeletePublisher;
using BookStore.Application.GetAllPublishers;
using BookStore.Application.GetPublisherById;
using BookStore.Application.UpdatePublisher;
using BookStore.Domain;
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

    [HttpPost("InsertPublisher")]
    public async Task<IActionResult> InsertPublisher([FromBody] CreatePublisherRequest request,
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisher(string id, CancellationToken cancellationToken)
    {
        var request = new DeletePublisherRequest { Id = id };
        var response = await this.mediator.Send(request, cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePublisher(string id, [FromBody] UpdatePublisherRequest request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        var response = await this.mediator.Send(request, cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest();
    }
}