
using MediatR;

namespace BookStore.Application.GetWeatherForecast
{
  public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastRequest, GetWeatherForecastResponse>
  {
    private static readonly string[] Summaries = new[]
{
          "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public async Task<GetWeatherForecastResponse> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
      return new GetWeatherForecastResponse()
      {
        Result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
          Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
          TemperatureC = Random.Shared.Next(-20, 55),
          Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
      .ToArray()
      };
    }
  }
}
