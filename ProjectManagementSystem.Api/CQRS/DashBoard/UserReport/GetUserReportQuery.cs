using MediatR;
using ProjectManagementSystem.Api.DTOs.DashBoard;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.DashBoard.UserReport
{
    public class GetUserReportQuery : IRequest<Result<UserReportDto>>
    {
        public string Email { get; set; }
    }
}
