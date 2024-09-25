using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.DTOs.ResultDTO;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;

namespace ProjectManagementSystem.Api.CQRS.User.BlockUser
{
    public record BlockUserCommand(int BlockedID) : IRequest<ResultDTO>;

    public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, ResultDTO>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BlockUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResultDTO> Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(Convert.ToString(request.BlockedID));

            if (user is null)
            {
                return ResultDTO.Failure("This user is not exists");
            }
            if (user is not null && user.UserStatus != UserStatus.NotActive)
            {
                return ResultDTO.Failure("User is already blocked.");
            }
            user.UserStatus = UserStatus.NotActive; 

            await _userManager.UpdateAsync(user);
            return ResultDTO.Success("User blocked successfully.");
        }
    }
}
