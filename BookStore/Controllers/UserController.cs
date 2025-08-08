using BookStore.Application.GetAllUsers;
using BookStore.Application.InsertUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterUser([FromBody] InsertUserRequest request,CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request,cancellationToken);
        if(response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllUsersRequest(),cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.OK ||
            response.StatusCode == System.Net.HttpStatusCode.Created)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}