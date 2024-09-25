using MediatR;
using ProjectManagementSystem.Api.DTOs.DashBoard;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.DashBoard.TaskReport
{
    public class GetTaskReportQuery : IRequest<Result<TaskReportDto>>
    {
        public string Email { get; set; }
    }
}
