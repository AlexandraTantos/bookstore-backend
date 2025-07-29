using BookStore.Application.GetWeatherForecast;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly IMediator mediator;

    public WeatherForecastController(IMediator mediator)
    {
      this.mediator = mediator;
    }

    [HttpGet, Route("GetWeatherController")]
    public async Task<IActionResult> GetWeatherController([FromQuery]GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
      var response = this.mediator.Send(request);
      return Ok(response);
    }
  }
}
