using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.DTOs.Auth;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Helpers.Paginations;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectManagementSystem.Api.CQRS.User.GetAllUsers.Queries
{
    public class GetUsersQuery : IRequest<Pagination<GetAllUsersDto>>
    {
        public string? Search { get; set; }
        public UserStatus? Status { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;


        public GetUsersQuery(string? search, UserStatus? status, int pageIndex = 1, int pageSize = 10)
        {
            Search = search;
            Status = status;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Pagination<GetAllUsersDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUsersQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Pagination<GetAllUsersDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {

            var usersQuery = _userManager.Users.AsQueryable();


            if (!string.IsNullOrEmpty(request.Search))
            {
                usersQuery = usersQuery.Where(u =>
                    u.UserName.Contains(request.Search) ||
                    u.Email.Contains(request.Search));
            }

            if (request.Status.HasValue)
            {
                usersQuery = usersQuery.Where(s => s.Status == request.Status.Value);
            }


            if (request.Status.HasValue)
            {
                usersQuery = usersQuery.Where(s => s.Status == request.Status.Value);
            }


            var totalUsers = usersQuery.Count();

            var users = usersQuery
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new GetAllUsersDto
                {
                    UserName = u.UserName,
                    Status = u.Status,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    DateCreated = u.DateCreated,

                })
                .ToList();
            if (users.Count == 0)
            {
                return new Pagination<GetAllUsersDto>(isSuccess: false, users, totalUsers, request.PageIndex, request.PageSize, message: "Users not found");
            }


            return new Pagination<GetAllUsersDto>(isSuccess: true, users, totalUsers, request.PageIndex, request.PageSize, message: "All users displays");
        }
    }
}
