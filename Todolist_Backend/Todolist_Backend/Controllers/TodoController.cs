using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.ViewModels;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodolistDbContext _context;

        public TodoController(TodolistDbContext context)
        {
            _context = context;
        }

        // 取得登入使用者的 Todo 列表
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] string? filter = "all")
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (userId == 0)
            {
                return Unauthorized("User is not authenticated.");
            }
                
            var query = _context.Todos.Where(t => t.UserId == userId);

            // 根據篩選條件進行查詢
            switch (filter?.ToLower())
            {
                case "completed":
                    query = query.Where(t => t.IsCompleted);
                    break;
                case "incomplete":
                    query = query.Where(t => t.DueDate > DateTime.UtcNow && !t.IsCompleted);
                    break;
                case "expired":
                    query = query.Where(t => t.DueDate < DateTime.UtcNow && !t.IsCompleted);
                    break;
                default:
                    break;
            }

            // 取得資料並返回
            var todos = await query.OrderByDescending(t => t.CreatedAt).ToListAsync();

            // 回傳 Todo 列表
            var todoModels = todos.Select(t => new TodoModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.IsCompleted ? "completed" : (t.DueDate < DateTime.UtcNow ? "expired" : "incomplete"),
                CreatedAt = t.CreatedAt,
                DueDate = t.DueDate
            }).ToList();

            return Ok(todoModels);
        }
    }
}
