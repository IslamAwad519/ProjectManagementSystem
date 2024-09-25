using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.CQRS.DashBoard.TaskReport;
using ProjectManagementSystem.Api.DTOs.DashBoard;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Helpers;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Api.CQRS.Dashboard.TaskReport
{
    public class GetTaskReportQueryHandler : IRequestHandler<GetTaskReportQuery, Result<TaskReportDto>>
    {
        private readonly IRepository<Models.TaskItem> _taskRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetTaskReportQueryHandler(
            IRepository<Models.TaskItem> taskRepository,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<TaskReportDto>> Handle(GetTaskReportQuery request, CancellationToken cancellationToken)
        {
            // Find the user by email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Result<TaskReportDto>.Failure("User not found.");
            }

            // Check if the user is a manager
            var isManager = await _userManager.IsInRoleAsync(user, AppRoles.Manager);
            if (!isManager)
            {
                return Result<TaskReportDto>.Failure("Unauthorized user.");
            }

            // Retrieve all tasks 
            var tasks = _taskRepository.GetAll().ToList();

            //The report data
            var totalProjects = tasks.Select(t => t.ProjectId).Distinct().Count();
            var totalTasks = tasks.Count();
            var completedTasks = tasks.Count(t => t.TaskStatus == TaskItemStatus.Done);
            var usersProgress = totalTasks > 0 ? (double)completedTasks / totalTasks * 100 : 0;

            var taskReportDto = new TaskReportDto
            {
                TotalProjects = totalProjects,
                TotalTasks = totalTasks,
                UsersProgress = usersProgress
            };

            return Result<TaskReportDto>.Success(taskReportDto, "Task report generated successfully.");
        }


    }

}

