using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.Models.Entities;
using Todolist_Backend.Models.DTOs.Login;
using Todolist_Backend.Services.Interfaces.Token;
using Todolist_Backend.Services.Interfaces.Login;

namespace Todolist_Backend.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly TodolistDbContext _context;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginService(TodolistDbContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<(bool Success, string Message, string? Token, User? User)> LoginAsync(LoginDTO model)
        {
            // 檢查 Email 是否存在
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return (false, "Email not registered.", null, null);
            }

            // 檢查密碼是否正確
            bool passwordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
            if (!passwordValid)
            {
                return (false, "Password incorrect.", null, null);
            }

            // 生成 JWT 驗證碼
            var token = _jwtTokenService.CreateJwtToken(user);
            return (true, "Login successful.", token, user);
        }
    }
}
