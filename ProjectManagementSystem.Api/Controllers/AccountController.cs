using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands;
using ProjectManagementSystem.Api.Dtos.ForgetPassword;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.Helpers;
using ProjectManagementSystem.Api.ViewModels.ForgetPassword;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using ProjectManagementSystem.Api.ViewModels.VerifyAccount;

namespace ProjectManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
}

