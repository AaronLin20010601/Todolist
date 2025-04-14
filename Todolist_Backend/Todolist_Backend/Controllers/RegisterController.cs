using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.Models.Entities;
using Todolist_Backend.Models.DTOs.Register;
using Todolist_Backend.Services.Interfaces.Email;
using Todolist_Backend.Services.Interfaces.Token;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly TodolistDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IVerificationCodeService _verificationCodeService;

        public RegisterController(TodolistDbContext context, IEmailService emailService, IVerificationCodeService verificationCodeService)
        {
            _context = context;
            _emailService = emailService;
            _verificationCodeService = verificationCodeService;
        }

        // 註冊流程的第一步 發送驗證碼到Email
        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] EmailDTO model)
        {
            // 檢查Email格式是否正確
            if (string.IsNullOrWhiteSpace(model.Email) || !model.Email.Contains('@'))
            {
                return BadRequest("Invalid email format.");
            }

            // 檢查Email是否已經註冊過
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already registered.");
            }

            // 生成驗證碼
            var verificationCode = _verificationCodeService.GenerateVerificationCode();

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
            var subject = "Your verification code";
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

        // 註冊流程的第二步 驗證驗證碼，並註冊用戶
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            // 檢查註冊資料是否有效
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Username) ||
                string.IsNullOrWhiteSpace(model.Password) || model.Password != model.ConfirmPassword)
            {
                return BadRequest("Invalid registration data.");
            }

            // 查找ResetToken，確認Email和驗證碼
            var resetToken = await _context.ResetTokens
                .FirstOrDefaultAsync(rt => rt.Email == model.Email && rt.Token == model.VerificationCode && !rt.IsUsed);

            if (resetToken == null || resetToken.ExpirationDate < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired verification code.");
            }

            // 註冊新用戶
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password), // 密碼加密
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 設置ResetToken為已使用
            resetToken.IsUsed = true;
            _context.ResetTokens.Update(resetToken);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
    }
}