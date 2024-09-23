using ProjectManagementSystem.Api.Exceptions.Error;
namespace ProjectManagementSystem.Api.ViewModels.ResultViewModel;


public class ResultViewModelDynamic
{
    public bool IsSuccess { get; set; }
    public dynamic Data { get; set; }
    public string Message { get; set; } = null!;
    public ErrorCode ErrorCode { get; set; }


    public static ResultViewModelDynamic Success(dynamic data, string message = "")
    {
        return new ResultViewModelDynamic
        {
            IsSuccess = true,
            Data = default,
            Message = message,
            ErrorCode = ErrorCode.NoError,
        };
    }

    public static ResultViewModelDynamic Failure(ErrorCode errorCode, string message)
    {
        return new ResultViewModelDynamic
        {
            IsSuccess = false,
            Data = default,
            Message = message,
            ErrorCode = errorCode,
        };
    }
}
