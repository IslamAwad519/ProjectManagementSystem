using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.CQRS.Project.Commands.CreateProject;
using ProjectManagementSystem.Api.CQRS.Project.Commands.DeleteProject;
using ProjectManagementSystem.Api.CQRS.Project.Commands.UpdateProject;
using ProjectManagementSystem.Api.CQRS.Project.Queries.GetById;
using ProjectManagementSystem.Api.CQRS.Project.Queries.GetList;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using System.Collections.Generic;

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
    public async Task<ResultViewModel<List<ProjectDto>>> GetAll([FromQuery] GetProjectListQueryParams projectParams)
    {
        var query = new GetProjectsQuery(projectParams.IsDescending,projectParams.Name, projectParams.Status, projectParams.CreatedFrom, 
            projectParams.CreatedTo,projectParams.OrderBy);
        var projects = await _mediator.Send(query);

        if (projects is null)
        {
            return new ResultViewModel<List<ProjectDto>>()
            {
                IsSuccess = true,
                Data = null,
                Message = "No Projects Found"
            };
        }
        return new ResultViewModel<List<ProjectDto>>()
        {
                IsSuccess = true,
                Data = projects,
                Message = "Request Success"
        };
    }

    [HttpGet("{id}")]
    public async Task<ResultViewModel<ProjectDto>> GetById(int id)
    {
        var query = new GetProjectQuery { ProjectId = id };
        var project = await _mediator.Send(query);
        if (project is null)
        {
            return new ResultViewModel<ProjectDto>()
            {
                IsSuccess = true,
                Data = null,
                Message = $"No Project with id {id} found"
            };
        }
        return new ResultViewModel<ProjectDto>()
        {
            IsSuccess = true,
            Data = project,
            Message = "Request Success"
        };
    }
    
    [HttpPost("create")]
    public async Task<ResultViewModel<ProjectDto>> Create(CreateProjectDto request)
    {
        var command = _mapper.Map<CreateProjectCommand>(request);
        var project = await _mediator.Send(command);
        if (project is null)
        {
            return new ResultViewModel<ProjectDto>()
            {
                IsSuccess = false,
                Data = null,
                Message = $"some error occured while creating project"
            };
        }
        return new ResultViewModel<ProjectDto>()
        {
            IsSuccess = true,
            Data = project,
            Message = "project created successfully"
        };
        //return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }
    
    [HttpPut("update")]
    public async Task<ResultViewModel<ProjectDto>> Update(UpdateProjectDto request)
    {
        var command = _mapper.Map<UpdateProjectCommand>(request);
        var project = await _mediator.Send(command);
    
        if (project is null)
        {
            return new ResultViewModel<ProjectDto>()
            {
                IsSuccess = false,
                Data = null,
                Message = $"some error occured while updating project"
            };
        }
        return new ResultViewModel<ProjectDto>()
        {
            IsSuccess = true,
            Data = project,
            Message = "project updated successfully"
        };
    }
    
    [HttpDelete("{id}")]
    public async Task<ResultViewModel<ProjectDto>> Delete(int id)
    {
        var command = new DeleteProjectCommand { ProjectId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return new ResultViewModel<ProjectDto>()
            {
                IsSuccess = false,
                Data = null,
                Message = $"some error occured while deleting project"
            };
        }
        return new ResultViewModel<ProjectDto>()
        {
            IsSuccess = true,
            Message = "project deleting successfully"
        };
    }
}
