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

        // 新增 Todo
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoCreateUpdateModel model)
        {
            // 檢查標題是否為空
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                return BadRequest("Title is required.");
            }

            // 檢查 DueDate 是否小於當前時間
            if (model.DueDate.HasValue && model.DueDate.Value < DateTime.UtcNow)
            {
                return BadRequest("Due date cannot be in the past.");
            }

            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (userId == 0)
            {
                return Unauthorized("User is not authenticated.");
            }

            // 建立 Todo
            var todo = new Todo
            {
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate?.ToUniversalTime() ?? DateTime.UtcNow.AddDays(1), // 若為 null 則預設明天
                IsCompleted = model.IsCompleted,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Todo created successfully!", id = todo.Id });
        }
    }
}
