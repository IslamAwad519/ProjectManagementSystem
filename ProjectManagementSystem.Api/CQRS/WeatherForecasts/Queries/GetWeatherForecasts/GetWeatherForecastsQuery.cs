using MediatR;
using ProjectManagementSystem.Api.Models;

namespace ProjectManagementSystem.Api.CQRS.WeatherForecasts.Queries.GetWeatherForecasts;
public record GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecast>>;

