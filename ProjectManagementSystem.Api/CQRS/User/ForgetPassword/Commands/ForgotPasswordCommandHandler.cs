using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.Dtos.ForgetPassword;
using ProjectManagementSystem.Api.Models;

namespace ProjectManagementSystem.Api.CQRS.User.ForgetPassword.Commands
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordResponseDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ProjectManagementSystem.Api.Services.ForgetPassword.IMailService _emailSender;

        public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager, ProjectManagementSystem.Api.Services.ForgetPassword.IMailService emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<ForgotPasswordResponseDto> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            // Null check for Email property directly
            if (string.IsNullOrEmpty(request.Email))
            {
                return new ForgotPasswordResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid request or email."
                };
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || user.IsDeleted)
            {
                return new ForgotPasswordResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email address."
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://yourapp.com/reset-password?token={token}&email={user.Email}";

            // Send email with the reset link
            await _emailSender.SendEmailAsync(user.Email, "Reset Password", $"Please reset your password using this link: {resetLink}");

            return new ForgotPasswordResponseDto
            {
                IsSuccess = true,
                Message = "Password reset link has been sent to your email.",
                ResetLink = resetLink
            };
        }

    }
}
