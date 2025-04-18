using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.Models.Entities;
using Todolist_Backend.Models.DTOs.Register;
using Todolist_Backend.Services.Interfaces.Register;

namespace Todolist_Backend.Services.Register
{
    public class RegisterService : IRegisterService
    {
        private readonly TodolistDbContext _context;

        public RegisterService(TodolistDbContext context) 
        { 
            _context = context; 
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDTO model)
        {
            // 查找ResetToken，確認Email和驗證碼
            var resetToken = await _context.ResetTokens
                .FirstOrDefaultAsync(rt => rt.Email == model.Email && rt.Token == model.VerificationCode && !rt.IsUsed);

            if (resetToken == null || resetToken.ExpirationDate < DateTime.UtcNow)
            {
                return (false, "Invalid or expired verification code.");
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

            return (true, "User registered successfully.");
        }
    }
}
