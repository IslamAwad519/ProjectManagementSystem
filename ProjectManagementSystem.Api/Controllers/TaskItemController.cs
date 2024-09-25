using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.CQRS.TaskItem.Commands;
using ProjectManagementSystem.Api.CQRS.TaskItem.Queries;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using ProjectManagementSystem.Api.ViewModels.TaskItemVM;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Helpers;
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


    [HttpGet("GetAll")]
    public async Task<ResultViewModel<IEnumerable<TaskItemVM>>> GetAllTasksInAllProjects()
    {
        var query = new GetAllTasksQuery();
        var tasksDto = await _mediator.Send(query);
        var tasksResponseVM = _mapper.Map<List<TaskItemVM>>(tasksDto);
        return ResultViewModel<IEnumerable<TaskItemVM>>.Success(tasksResponseVM, "Tasks retrieved successfully.");
    }

    [HttpGet("{id}")]
    public async Task<ResultViewModel<TaskItemVM>> GetTaskById([FromRoute] int id)
    {
        var query = new GetTaskByIdQuery { Id = id };
        var taskDto = await _mediator.Send(query);

        if (taskDto == null)
        {
            return ResultViewModel<TaskItemVM>.Failure(ErrorCode.ResourceNotFound, "Task not found.");
        }
        var taskResponseVM = _mapper.Map<TaskItemVM>(taskDto);
        return ResultViewModel<TaskItemVM>.Success(taskResponseVM, "Task retrieved successfully.");
    }


    [HttpPut("{id}")]
    public async Task<ResultViewModel<TaskItemVM>> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
    {
        // Validate TaskId from the route and request body
        if (id != dto.TaskId)
        {
            return ResultViewModel<TaskItemVM>.Failure(ErrorCode.ValidationError, "Task ID mismatch");
        }

        // Validate TaskStatus 
        if ((int)dto.TaskStatus < 0 || (int)dto.TaskStatus > 2)
        {
            return ResultViewModel<TaskItemVM>.Failure(ErrorCode.ValidationError, "Invalid task status");
        }

        var command = new UpdateTaskCommand
        {
            TaskId = dto.TaskId,
            Title = dto.Title,
            Description = dto.Description,
            TaskStatus = dto.TaskStatus,
            UserId = dto.UserId
        };

        var updatedTaskDto = await _mediator.Send(command);
        if (updatedTaskDto == null)
        {
            return ResultViewModel<TaskItemVM>.Failure(ErrorCode.ResourceNotFound, "Task not found");
        }
        var updatedTaskVM = _mapper.Map<TaskItemVM>(updatedTaskDto);

        return ResultViewModel<TaskItemVM>.Success(updatedTaskVM, "Task updated successfully");
    }



    [HttpDelete("{id}")]
    public async Task<ResultViewModel<bool>> DeleteTask([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteTaskCommand(id));

        if (result.IsSuccess)
        {
            return ResultViewModel<bool>.Success(true, result.Message);
        }
        return ResultViewModel<bool>.Failure(result.ErrorCode, result.Message);
    }


    [HttpPost("createTask")]
    public async Task<ResultViewModel<TaskItemVM>> CreateTask(CreateTaskDto request)
    {
        var projects = await _mediator.Send(new GetProjectListForTasksQuery());
        var selectedProjects = projects.Where(p => request.ProjectIds.Contains(p.Id)).ToList();
        if (selectedProjects.Count != request.ProjectIds.Count)
        {
            return ResultViewModel<TaskItemVM>.Failure(ErrorCode.ValidationError, "One or more selected projects do not exist.");
        }
        var users = await _mediator.Send(new GetUserListForTasksQuery { ProjectIds = request.ProjectIds });
        var assignedUsers = users.Where(u => request.AssignedUserIds.Contains(u.Id)).ToList();
        if (assignedUsers.Count != request.AssignedUserIds.Count)
        {
            return ResultViewModel<TaskItemVM>.Failure(ErrorCode.ValidationError, "One or more users not found.");
        }

        var command = _mapper.Map<CreateTaskCommand>(request);
        var taskDto = await _mediator.Send(command);
        var taskResponseVM = _mapper.Map<TaskItemVM>(taskDto);
        return ResultViewModel<TaskItemVM>.Success(taskResponseVM, "Task created successfully.");
    }


    [HttpGet("search")]
    public async Task<ResultViewModel<IEnumerable<TaskItemDto>>> SearchTaskByTaskName([FromQuery] string taskName)
    {
        var result = await _mediator.Send(new SearchTaskByTaskNameQuery(taskName));
        if (result.IsSuccess)
        {
            return ResultViewModel<IEnumerable<TaskItemDto>>.Success(result.Data, result.Message);
        }

        return ResultViewModel<IEnumerable<TaskItemDto>>.Failure(result.ErrorCode, result.Message);
    }

    [HttpGet("filter")]
    public async Task<ResultViewModel<List<TaskItemDto>>> FilterTasks([FromQuery] string? projectName, [FromQuery] TaskItemStatus? taskStatus)
    {
        var query = new FilterTasksQuery(projectName, taskStatus);
        // Send the query to the mediator and get the result
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
        {
            return ResultViewModel<List<TaskItemDto>>.Success(result.Data, result.Message);
        }
        return ResultViewModel<List<TaskItemDto>>.Failure(result.ErrorCode, result.Message);
    }


    [HttpPut]
    public async Task<ResultViewModel<int>> ChangeTaskStatus(TaskStatusViewModel taskStatusViewModel)
    {
        var taskStatusDTO = taskStatusViewModel.MapOne<TaskItemStatusDTO>();

        var resultDTO = await _mediator.Send(new ChangeTaskStatusCommand(taskStatusDTO));

        if (!resultDTO.IsSuccess)
        {
            return ResultViewModel<int>.Failure(ErrorCode.BadRequest, resultDTO.Message);
        }

        return ResultViewModel<int>.Success(resultDTO.Data);
    }

}


