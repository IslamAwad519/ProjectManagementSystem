﻿namespace ProjectManagementSystem.Api.Dtos.ForgetPassword
{
    public class ForgotPasswordResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ResetLink { get; set; }
    }
}
