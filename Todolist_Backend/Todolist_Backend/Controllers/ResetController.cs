using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.ViewModels;
using Todolist_Backend.Services;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetController : ControllerBase
    {
        private readonly TodolistDbContext _context;
        private readonly IEmailService _emailService;
        private readonly TokenService _tokenService;

        public ResetController(TodolistDbContext context, IEmailService emailService, TokenService tokenService)
        {
            _context = context;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        // 重設密碼流程第一步 輸入已註冊的Email，發送驗證碼
        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] EmailModel model)
        {
            // 檢查Email格式是否正確
            if (string.IsNullOrWhiteSpace(model.Email) || !model.Email.Contains('@'))
            {
                return BadRequest("Invalid email format.");
            }

            // 檢查Email是否已經註冊過
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser == null)
            {
                return BadRequest("Email is not registered.");
            }

            // 生成驗證碼
            var verificationCode = _tokenService.GenerateVerificationCode();

            // 生成 ResetToken 並存入資料庫
            var resetToken = new ResetToken
            {
                Email = model.Email,
                Token = verificationCode,
                ExpirationDate = DateTime.UtcNow.AddMinutes(10), // 驗證碼有效時間為10分鐘
                IsUsed = false
            };
            _context.ResetTokens.Add(resetToken);
            await _context.SaveChangesAsync();

            // 發送驗證碼至使用者的Email
            var subject = "Password Reset Verification Code";
            var body = $"Your verification code is {verificationCode}. It is valid for 10 minutes.";
            var toEmails = new List<string> 
            { 
                model.Email
            };
            bool emailSent = await _emailService.SendEmailAsync(toEmails, subject, body);

            if (!emailSent)
            {
                return StatusCode(500, "Failed to send verification code.");
            }

            return Ok("Verification code sent.");
        }

        // 重設密碼流程第二步 輸入驗證碼與新密碼，完成重設
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetModel model)
        {
            // 檢查重設密碼是否相同
            if (string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                model.Password != model.ConfirmPassword)
            {
                return BadRequest("Invalid reset data.");
            }

            // 查找ResetToken，確認Email和驗證碼
            var resetToken = await _context.ResetTokens
                .FirstOrDefaultAsync(rt => rt.Email == model.Email && rt.Token == model.VerificationCode && !rt.IsUsed);

            if (resetToken == null || resetToken.ExpirationDate < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired verification code.");
            }

            // 檢查是否有該用戶
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // 更新密碼
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            _context.Users.Update(user);

            // 設置ResetToken為已使用
            resetToken.IsUsed = true;
            _context.ResetTokens.Update(resetToken);
            await _context.SaveChangesAsync();

            return Ok("Password has been reset successfully.");
        }
    }
}
