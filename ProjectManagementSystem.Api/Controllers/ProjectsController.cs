using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.CQRS.Project.Commands.CreateProject;
using ProjectManagementSystem.Api.CQRS.Project.Commands.DeleteProject;
using ProjectManagementSystem.Api.CQRS.Project.Commands.UpdateProject;
using ProjectManagementSystem.Api.CQRS.Project.Queries.GetById;
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
    public async Task<IActionResult> GetAll([FromQuery] GetProjectListQueryParams projectParams)
    {
        //var listParams = _mapper.Map<GetProjectsQuery>(projectParams);
        var query = new GetProjectsQuery(projectParams.IsDescending,projectParams.Name, projectParams.Status, projectParams.CreatedFrom, 
            projectParams.CreatedTo,projectParams.OrderBy);
        var projects = await _mediator.Send(query);
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetProjectQuery { ProjectId = id };
        var project = await _mediator.Send(query);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateProjectDto request)
    {
        var command = _mapper.Map<CreateProjectCommand>(request);
        var project = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateProjectDto request)
    {
        var command = _mapper.Map<UpdateProjectCommand>(request);
        var project = await _mediator.Send(command);
        if (project is null)
        {
            return NotFound();
        }
        return Ok(project);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteProjectCommand { ProjectId = id };
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
