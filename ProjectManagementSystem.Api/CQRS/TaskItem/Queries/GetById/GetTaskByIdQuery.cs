using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries.GetById
{
    public class GetTaskByIdQuery : IRequest<Result<TaskItemDto>>
    {
        public int Id { get; set; }
    }

  
}
