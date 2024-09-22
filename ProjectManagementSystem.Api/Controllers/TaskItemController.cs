using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.CQRS.TaskItem.Commands;
using ProjectManagementSystem.Api.CQRS.TaskItem.Queries;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.CQRS.TaskItem.Commands.CreateTask;
using ProjectManagementSystem.Api.CQRS.TaskItem.Commands.UpdateTask;
using ProjectManagementSystem.Api.CQRS.TaskItem.Commands.DeleteTask;
using ProjectManagementSystem.Api.CQRS.TaskItem.Queries.GetList;
using ProjectManagementSystem.Api.Dtos.Tasks;
using ProjectManagementSystem.Api.CQRS.TaskItem.Queries.GetById;
namespace ProjectManagementSystem.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TaskController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<ResultViewModel<List<TaskItemDto>>> GetAll([FromQuery] GetTaskListQueryParams taskParams)
    {
        var query = new GetAllTasksQuery(taskParams.IsDescending, taskParams.Name, taskParams.Status, taskParams.CreatedFrom,
            taskParams.CreatedTo, taskParams.OrderBy);
        var result = await _mediator.Send(query);

        return new ResultViewModel<List<TaskItemDto>>()
        {
            IsSuccess = result.IsSuccess,
            Data = result.Data,
            Message = result.Message
        };
    }


    [HttpGet("{id}")]
    public async Task<ResultViewModel<TaskItemDto>> GetById(int id)
    {
        var query = new GetTaskByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        return new ResultViewModel<TaskItemDto>()
        {
            IsSuccess = result.IsSuccess,   
            Data = result.Data,             
            Message = result.Message        
        };
    }


    [HttpPut("update")]
    public async Task<ResultViewModel<TaskItemDto>> Update(UpdateTaskDto request)
    {
        var command = _mapper.Map<UpdateTaskCommand>(request);
        var result = await _mediator.Send(command);

        return new ResultViewModel<TaskItemDto>
        {
            IsSuccess = result.IsSuccess,
            Data = result.Data,
            Message = result.Message
        };
    }

    [HttpDelete("{id}")]
    public async Task<ResultViewModel<TaskItemDto>> Delete(int id)
    {
        var command = new DeleteTaskCommand { TaskId = id };
        var result = await _mediator.Send(command);

        return new ResultViewModel<TaskItemDto>()
        {
            IsSuccess = result.IsSuccess,
            Message = result.Message
        };
    }

    [HttpPost("create")]
    public async Task<ResultViewModel<TaskItemDto>> Create(CreateTaskDto request)
    {
        // Ensure lists are not empty
        if (!request.ProjectIds.Any() || !request.AssignedUserIds.Any())
        {
            return new ResultViewModel<TaskItemDto>
            {
                IsSuccess = false,
                Message = "ProjectId and UserId cannot be empty."
            };
        }

        var command = _mapper.Map<CreateTaskCommand>(request);
        var result = await _mediator.Send(command);

        return new ResultViewModel<TaskItemDto>
        {
            IsSuccess = result.IsSuccess,
            Data = result.Data,
            Message = result.Message
        };
    }





}


