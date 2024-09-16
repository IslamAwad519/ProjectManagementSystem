using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.CQRS.User.ChangePassword.Commands;
using ProjectManagementSystem.Api.CQRS.User.Login.Queries;
using ProjectManagementSystem.Api.DTOs.Auth;

namespace ProjectManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public AccountController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto request)
    {
        var query = _mapper.Map<LoginUserQuery>(request);

        var result = await _mediator.Send(query);
        if (result is null)
        {
            return Unauthorized("Password or username is wrong");
        }

        return Ok(result);
    }
    //[Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        var command = _mapper.Map<ChangePasswordCommand>(request);

        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok("Password changed successfully");
        }
        return BadRequest("Something went wrong !");
    }
    
}
