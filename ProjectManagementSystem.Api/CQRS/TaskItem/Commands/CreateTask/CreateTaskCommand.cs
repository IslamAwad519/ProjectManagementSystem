using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Api.CQRS.TaskItem.Queries;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<Result<TaskItemDto>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<int> ProjectIds { get; set; }
        public List<int> AssignedUserIds { get; set; }


    }

  
}


