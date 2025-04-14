using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.Models.DTOs;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TodolistDbContext _context;

        public AccountController(TodolistDbContext context)
        {
            _context = context;
        }

        // 取得帳號可編輯資料
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAccount()
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (userId == 0)
            {
                return Unauthorized("User is not authenticated.");
            }

            // 找出用戶資料
            var user = await _context.Users.FindAsync(userId);
            if (user == null) 
            { 
                return NotFound("User not found."); 
            }

            var accountModel = new AccountDTO
            {
                Username = user.Username
            };

            return Ok(accountModel);
        }

        // 編輯帳號資料
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountDTO model)
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (userId == 0) 
            { 
                return Unauthorized("User is not authenticated."); 
            }

            // 找出用戶資料
            var user = await _context.Users.FindAsync(userId);
            if (user == null) 
            { 
                return NotFound("User not found."); 
            }

            if (string.IsNullOrWhiteSpace(model.Username))
            {
                return BadRequest("Username cannot be empty.");
            }

            user.Username = model.Username;
            await _context.SaveChangesAsync();

            return Ok("Username updated successfully.");
        }

        // 刪除帳號
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccount()
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (userId == 0) 
            { 
                return Unauthorized("User is not authenticated."); 
            }

            // 找出用戶資料
            var user = await _context.Users.FindAsync(userId);
            if (user == null) 
            { 
                return NotFound("User not found."); 
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("Account deleted successfully.");
        }
    }
}
