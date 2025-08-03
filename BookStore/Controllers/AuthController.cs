using System.Net;
using BookStore.Application.LoginUser;
using BookStore.Application.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(response);
        }
        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Unauthorized(response);
        }
        else
        {
            return BadRequest(response);
        }
    }
    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var response = await mediator.Send(request);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return Unauthorized(new { message = response.Message });
        }

        return Ok(new
        {
            accessToken = response.AccessToken,
            refreshToken = response.RefreshToken,
            message = response.Message
        });
    }
}

