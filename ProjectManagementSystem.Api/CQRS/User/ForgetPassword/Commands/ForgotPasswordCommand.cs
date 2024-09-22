using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ProjectManagementSystem.Api.Dtos.ForgetPassword;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using System.Threading;
using System.Threading.Tasks;

public class ForgotPasswordCommand : IRequest<ForgotPasswordResponseDto>
{
    public string Email { get; set; }
}



