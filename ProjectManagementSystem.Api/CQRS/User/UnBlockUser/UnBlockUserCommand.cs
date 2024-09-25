using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.DTOs.ResultDTO;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;

namespace ProjectManagementSystem.Api.CQRS.User.UnBlockUser
{
    public record UnBlockUserCommand(int BlockedID) : IRequest<ResultDTO>;

    public class UnBlockUserCommandHandler : IRequestHandler<UnBlockUserCommand, ResultDTO>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UnBlockUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResultDTO> Handle(UnBlockUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(Convert.ToString(request.BlockedID));

            if (user is null)
            {
                return ResultDTO.Failure("This user is not exists");
            }
            if (user is not null && user.UserStatus != UserStatus.Active)
            {
                return ResultDTO.Failure("User is already not blocked.");
            }
            user.UserStatus = UserStatus.Active; 

            await _userManager.UpdateAsync(user);
            return ResultDTO.Success("User unblocked successfully.");
        }
    }
}
