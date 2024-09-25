using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.CQRS.DashBoard.TaskReport;
using ProjectManagementSystem.Api.CQRS.DashBoard.UserReport;
using ProjectManagementSystem.Api.DTOs.DashBoard;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DashBoardController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("task-report")]
        public async Task<ResultViewModel<TaskReportDto>> GetTaskReport(string email)
        {
            var query = new GetTaskReportQuery
            {
                Email = email
            };

            var result = await _mediator.Send(query);

            return new ResultViewModel<TaskReportDto>()
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data,
                Message = result.Message
            };
        }


        [HttpGet("user-report")]
        public async Task<ResultViewModel<UserReportDto>> GetUserReport(string email)
        {
            var query = new GetUserReportQuery
            {
                Email = email
            };

            var result = await _mediator.Send(query);

            return new ResultViewModel<UserReportDto>()
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data,
                Message = result.Message
            };
        }

    }
}