using MediatR;
using ProjectManagementSystem.Api.Models;


namespace ProjectManagementSystem.Api.CQRS.WeatherForecasts.Queries.GetWeatherForecasts;
public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery,IEnumerable<WeatherForecast>>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public async Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
    {
        var rng = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        });
    }
}
