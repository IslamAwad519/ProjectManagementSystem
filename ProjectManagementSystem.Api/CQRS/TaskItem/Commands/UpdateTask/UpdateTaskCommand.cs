using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using System.Linq;
using ProjectManagementSystem.Api.Exceptions.Error;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<Result<TaskItemDto>>
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemStatus TaskStatus { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }

  
}

