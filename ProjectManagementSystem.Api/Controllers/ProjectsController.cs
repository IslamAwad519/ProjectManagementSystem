using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.CQRS.Project.Commands.CreateProject;
using ProjectManagementSystem.Api.CQRS.Project.Queries.GetList;
using ProjectManagementSystem.Api.DTOs.Projects;

namespace ProjectManagementSystem.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;    
    public ProjectsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _mediator.Send(new GetProjectsQuery());
        return Ok(projects);
    }
    
    [HttpPost("create-project")]
    public async Task<IActionResult> Create(CreateProjectDto request)
    {
        var command = _mapper.Map<CreateProjectCommand>(request);
        var project = await _mediator.Send(command);
        return Ok(project);
    }
}
