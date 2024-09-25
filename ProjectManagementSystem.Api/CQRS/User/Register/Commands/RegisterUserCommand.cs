using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.DTOs.ResultDTO;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.Services.VerifyAccount;

namespace ProjectManagementSystem.Api.CQRS.User.Register.Commands
{
    public record RegisterUserCommand(RegisterUserRequestDTO registerUserRequestDTO) : IRequest<ResultDTO>;
    public record RegisterUserRequestDTO( string UserName, string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword);

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResultDTO>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOTPService _otpService;
        private readonly IRepository<ProjectManagementSystem.Api.Models.User> _userRepository;



        public  RegisterUserCommandHandler( IMediator mediator , UserManager<ApplicationUser> userManager ,
            IOTPService oTPService , IRepository<ProjectManagementSystem.Api.Models.User> repository) 
        {
            _mediator = mediator;
           _userManager = userManager;
            _otpService = oTPService;
            _userRepository = repository;
            
        }
        public async Task<ResultDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if(request.registerUserRequestDTO.Password != request.registerUserRequestDTO.ConfirmPassword)
            {
                return ResultDTO.Failure("PassWord and ConfirmPassword must be equal");
            }
            var User = await _userManager.FindByEmailAsync(request.registerUserRequestDTO.Email);

            if (User is not null || User.IsDeleted  )
            {
                return ResultDTO.Failure("The Email is registered!");
            }
            User = await _userManager.FindByNameAsync(request.registerUserRequestDTO.UserName);
            if (User is not null || User.IsDeleted)
            {
                return ResultDTO.Failure("The Username is registered!");
            }

            var newUser = new ApplicationUser
            {
                UserName = request.registerUserRequestDTO.UserName,
                Email = request.registerUserRequestDTO.Email,
                PhoneNumber = request.registerUserRequestDTO.PhoneNumber
            };

           var result=await _userManager.CreateAsync(newUser , request.registerUserRequestDTO.Password);
            

            if (result.Succeeded)
            {
                var opt = await _otpService.GenerateAndSendOTPAsync(newUser.Email);

                //_userRepository.Add(new Models.User() { UserName = newUser.UserName , IsDeleted=false });
                //_userRepository.SaveChanges();

                return ResultDTO.Success("User registered successfully");

            }
            return ResultDTO.Failure("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));



        }
    }


}
