using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.DTOs.DashBoard;
using ProjectManagementSystem.Api.Helpers;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Api.CQRS.DashBoard.UserReport
{
    public class GetUserReportQueryHandler : IRequestHandler<GetUserReportQuery, Result<UserReportDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetUserReportQueryHandler(
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<UserReportDto>> Handle(GetUserReportQuery request, CancellationToken cancellationToken)
        {
            // Find the user by email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Result<UserReportDto>.Failure("User not found.");
            }

            // Check if the user is a manager
            var isManager = await _userManager.IsInRoleAsync(user, AppRoles.Manager);
            if (!isManager)
            {
                return Result<UserReportDto>.Failure("Unauthorized user.");
            }

            // Get all users
            var users = _userManager.Users.ToList();

            //The report data
            var totalActiveUsers = users.Count(u => !u.IsDeleted); 
            var totalInactiveUsers = users.Count(u => u.IsDeleted);

            var userReportDto = new UserReportDto
            {
                TotalActiveUsers = totalActiveUsers,
                TotalInactiveUsers = totalInactiveUsers
            };

            return Result<UserReportDto>.Success(userReportDto, "User report generated successfully.");
        }
    }
}
