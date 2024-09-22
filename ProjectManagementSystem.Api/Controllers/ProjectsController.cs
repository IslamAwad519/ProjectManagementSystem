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
        var result = await _mediator.Send(query);

        return new ResultViewModel<List<ProjectDto>>()
        {
                IsSuccess = result.IsSuccess,
                Data = result.Data,
                Message = result.Message
        };
    }

    [HttpGet("{id}")]
    public async Task<ResultViewModel<ProjectDto>> GetById(int id)
    {
        var query = new GetProjectQuery { ProjectId = id };
        var result = await _mediator.Send(query);
  
        return new ResultViewModel<ProjectDto>()
        {
            IsSuccess = result.IsSuccess,
            Data = result.Data,
            Message = result.Message
        };
    }
    

    [HttpPost("create")]
    public async Task<ResultViewModel<ProjectDto>> Create(CreateProjectDto request)
    {
        var command = _mapper.Map<CreateProjectCommand>(request);
        var result = await _mediator.Send(command);
     
        return new ResultViewModel<ProjectDto>()
        {
            IsSuccess =result.IsSuccess, 
            Data = result.Data,
            Message = result.Message
        };
    }
    
    [HttpPut("update")]
    public async Task<ResultViewModel<ProjectDto>> Update(UpdateProjectDto request)
    {
        var command = _mapper.Map<UpdateProjectCommand>(request);
        var result = await _mediator.Send(command);

        return new ResultViewModel<ProjectDto>()
        {
            IsSuccess = result.IsSuccess,
            Data = result.Data,
            Message = result.Message
        };
    }
    
    [HttpDelete("{id}")]
    public async Task<ResultViewModel<ProjectDto>> Delete(int id)
    {
        var command = new DeleteProjectCommand { ProjectId = id };
        var result = await _mediator.Send(command);

        return new ResultViewModel<ProjectDto>()
        {
            IsSuccess = result.IsSuccess,
            Message = result.Message
        };
    }
}
