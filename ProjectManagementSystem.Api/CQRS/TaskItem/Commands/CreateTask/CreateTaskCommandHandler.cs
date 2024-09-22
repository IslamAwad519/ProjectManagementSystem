using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.CQRS.TaskItem.Queries;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using ProjectManagementSystem.Api.CQRS.TaskItem.Commands.CreateTask;
using ProjectManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;


public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<TaskItemDto>>
{
    private readonly IRepository<TaskItem> _taskRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Project> _projectRepository;
    private readonly IMapper _mapper;

    public CreateTaskCommandHandler(
        IRepository<TaskItem> taskRepository,
        IRepository<User> userRepository,
        IRepository<Project> projectRepository,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<Result<TaskItemDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // Check ProjectIds and AssignedUserIds are not empty
        if (!request.ProjectIds.Any() || !request.AssignedUserIds.Any())
        {
            return Result<TaskItemDto>.Failure("ProjectId and UserId cannot be empty.");
        }

        // Fetch users for the selected project
        var projectId = request.ProjectIds.First();
        var usersInProject = await _userRepository.GetAll() //Get all users
            .Where(u => u.Projects.Any(p => p.ProjectId == projectId))
            .ToListAsync(cancellationToken); 

        // Check if the assigned user is in the list of users for the selected project
        if (!usersInProject.Any(u => u.Id == request.AssignedUserIds.First()))
        {
            return Result<TaskItemDto>.Failure("The selected user is not assigned to the selected project.");
        }

        // Map command to TaskItem
        var taskItem = _mapper.Map<TaskItem>(request);

        _taskRepository.Add(taskItem);
        _taskRepository.SaveChanges(); 

        // Fetch project and user details 
        var project = _projectRepository.GetByID(projectId);
        var user = _userRepository.GetByID(taskItem.UserId);

        // set names
        var taskItemDto = _mapper.Map<TaskItemDto>(taskItem);
        taskItemDto.ProjectName = project?.Name;
        taskItemDto.UserName = user?.UserName;

        return Result<TaskItemDto>.Success(taskItemDto, "Task created successfully.");
    }







}









