using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.Models.DTOs.Reset;
using Todolist_Backend.Services.Interfaces.Reset;

namespace Todolist_Backend.Services.Reset
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly TodolistDbContext _context;

        public ResetPasswordService(TodolistDbContext context) 
        { 
            _context = context;
        }

        public async Task<(bool Success, string Message)> ResetPasswordAsync(ResetDTO model)
        {
            // 檢查重設密碼是否相同
            if (string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                model.Password != model.ConfirmPassword)
            {
                return (false, "Invalid reset data.");
            }

            // 查找ResetToken，確認Email和驗證碼
            var resetToken = await _context.ResetTokens
                .FirstOrDefaultAsync(rt => rt.Email == model.Email && rt.Token == model.VerificationCode && !rt.IsUsed);

            if (resetToken == null || resetToken.ExpirationDate < DateTime.UtcNow)
            {
                return (false, "Invalid or expired verification code.");
            }

            // 檢查是否有該用戶
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return (false, "User not found.");
            }

            // 更新密碼
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            _context.Users.Update(user);

            // 設置ResetToken為已使用
            resetToken.IsUsed = true;
            _context.ResetTokens.Update(resetToken);
            await _context.SaveChangesAsync();

            return (true, "Password has been reset successfully.");
        }
    }
}
