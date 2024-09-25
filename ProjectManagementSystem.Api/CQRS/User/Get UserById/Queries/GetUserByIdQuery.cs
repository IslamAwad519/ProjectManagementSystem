using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.DTOs.Auth;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.User.GetUserById.Queries
{
    public class GetUserByIdQuery : IRequest<Result<GetUserByIdDto>>
    {
        public string Id { get; set; }
        public GetUserByIdQuery(string id)
        {
            Id = id; 
        }

      
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserByIdQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<GetUserByIdDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null) return Result<GetUserByIdDto>.Failure($"User with ID {request.Id} not found");
            var userDto = new GetUserByIdDto()
            {
                Id =user.Id,
                UserName =user.UserName,
                Status = user.Status,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateCreated = user.DateCreated,
                

            };
            return Result<GetUserByIdDto>.Success(userDto);
        }
    }

}
