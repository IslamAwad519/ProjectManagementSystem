using ProjectManagementSystem.Api.Services.ForgetPassword;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Api.Services.VerifyAccount
{
    public class OTPService : IOTPService
    {
        private readonly IMailService _mailService;

        // Assume this is a temporary store for OTPs. Replace with a real database or cache.
        private static readonly Dictionary<string, (string otpCode, DateTime expiryDate)> OtpStore = new();

        public OTPService(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task<string> GenerateAndSendOTPAsync(string email)
        {
            var otpCode = new Random().Next(100000, 999999).ToString(); // Generate a 6-digit OTP
            var expiryDate = DateTime.UtcNow.AddMinutes(1); // OTP valid for 1 minute

            // Save OTP to temporary store 
            OtpStore[email] = (otpCode, expiryDate);

            // Send OTP via email
            await _mailService.SendEmailAsync(email, "Your OTP Code", $"Your OTP code is: {otpCode}");

            return otpCode;
        }

        public async Task<bool> VerifyOTPAsync(string email, string otpCode)
        {
            if (OtpStore.TryGetValue(email, out var otpData))
            {
                if (otpData.expiryDate > DateTime.UtcNow && otpData.otpCode == otpCode)
                {
                    // OTP is valid and not expired
                    OtpStore.Remove(email); // Remove OTP after successful verification
                    return true;
                }
            }

            return false;
        }
    }
}
