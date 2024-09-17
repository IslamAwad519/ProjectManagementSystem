using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.CQRS.WeatherForecasts.Queries.GetWeatherForecasts;
using ProjectManagementSystem.Api.Models;

namespace ProjectManagementSystem.Api.Controllers;
[ApiController]
[Route("[controller]")]
//[Authorize]
[ApiExplorerSettings(IgnoreApi = true)]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator mediator;

    public WeatherForecastController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
    {
        var query = new GetWeatherForecastsQuery();
        return await mediator.Send(query);
    }
}
