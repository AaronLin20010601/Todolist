using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.Models.Entities;
using Todolist_Backend.Services.Interfaces.Email;
using Todolist_Backend.Services.Interfaces.VerifyCode;

namespace Todolist_Backend.Services.VerifyCode
{
    public class SendRegisterVerifyService : ISendRegisterVerifyService
    {
        private readonly TodolistDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IVerificationCodeService _verificationCodeService;

        public SendRegisterVerifyService(TodolistDbContext context, IEmailService emailService, IVerificationCodeService verificationCodeService)
        {
            _context = context;
            _emailService = emailService;
            _verificationCodeService = verificationCodeService;
        }

        public async Task<(bool Success, string Message)> SendVerificationCodeAsync(string email)
        {
            // 檢查Email格式是否正確
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            {
                return (false, "Invalid email format.");
            }

            // 檢查Email是否已經註冊過
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                return (false, "Email is already registered.");
            }

            // 生成驗證碼
            var verificationCode = _verificationCodeService.GenerateVerificationCode();

            // 生成 ResetToken 並存入資料庫
            var resetToken = new ResetToken
            {
                Email = email,
                Token = verificationCode,
                ExpirationDate = DateTime.UtcNow.AddMinutes(10), // 驗證碼有效時間為10分鐘
                IsUsed = false
            };
            _context.ResetTokens.Add(resetToken);
            await _context.SaveChangesAsync();

            // 發送驗證碼至使用者的Email
            var subject = "Your verification code";
            var body = $"Your verification code is {verificationCode}. It is valid for 10 minutes.";
            bool emailSent = await _emailService.SendEmailAsync(new List<string> { email }, subject, body);

            if (!emailSent)
            {
                return (false, "Failed to send verification code.");
            }

            return (true, "Verification code sent.");
        }
    }
}
