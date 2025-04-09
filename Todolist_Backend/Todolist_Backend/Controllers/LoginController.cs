using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.Services;
using Todolist_Backend.ViewModels;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TodolistDbContext _context;
        private readonly TokenService _tokenService;

        public LoginController(TodolistDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // 檢查 Email 是否存在
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return BadRequest("Email not registered.");
            }

            // 檢查密碼是否正確
            bool passwordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
            if (!passwordValid)
            {
                return BadRequest("Password incorrect.");
            }

            // 生成 JWT 驗證碼
            var token = _tokenService.CreateJwtToken(user);

            // 登入成功
            return Ok(new
            {
                message = "Login successful.",
                token,
                user = new
                {
                    user.Id,
                    user.Username,
                    user.Email
                }
            });
        }
    }
}
