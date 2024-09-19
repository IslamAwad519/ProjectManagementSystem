namespace ProjectManagementSystem.Api.ViewModels.ResultViewModel;


public class Result 
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public static Result Success(string message = "")
    {
        return new Result { IsSuccess = true, Message = message };
    }

    public static Result Failure(string message)
    {
        return new Result { IsSuccess = false, Message = message };
    }
}

// Generic Result class for returning data
public class Result<T> : Result
{
    public T Data { get; set; }

    public static Result<T> Success(T data, string message = "")
    {
        return new Result<T> { IsSuccess = true, Data = data, Message = message };
    }

    public static Result<T> Failure(string message)
    {
        return new Result<T> { IsSuccess = false, Data = default, Message = message };
    }
}
