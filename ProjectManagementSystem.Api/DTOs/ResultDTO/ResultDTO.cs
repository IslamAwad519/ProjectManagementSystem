namespace ProjectManagementSystem.Api.DTOs.ResultDTO
{
    public class ResultDTO
    {
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }

        public static ResultDTO Success(dynamic data, string message = "Success Operation")
        {
            return new ResultDTO
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }

        public static ResultDTO Failure(string message = "Failed Operation")
        {
            return new ResultDTO
            {
                IsSuccess = false,
                Data = default,
                Message = message
            };
        }
    }
}
