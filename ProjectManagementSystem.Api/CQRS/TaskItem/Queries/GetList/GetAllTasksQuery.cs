using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using TaskStatusEnum = ProjectManagementSystem.Api.Enums.TaskItemStatus;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries.GetList
{
    public class GetAllTasksQuery : IRequest<Result<List<TaskItemDto>>>
    {
        public string? Name { get; set; } // For searching by name
        public TaskItemStatus? Status { get; set; }
        public DateTime? CreatedFrom { get; set; } // Start date 
        public DateTime? CreatedTo { get; set; }   // End date 
        public string? OrderBy { get; set; }  //ordering by Name, CreationTime
        public bool IsDescending { get; set; } = false;

        public GetAllTasksQuery(
            bool isDescending,
            string? name = null,
            TaskItemStatus? status = null,
            DateTime? createdFrom = null,
            DateTime? createdTo = null,
            string? orderBy = null
             )
        {
            Name = name;
            Status = status;
            CreatedFrom = createdFrom;
            CreatedTo = createdTo;
            OrderBy = orderBy;
            IsDescending = isDescending;
        }
    }

 
    }





