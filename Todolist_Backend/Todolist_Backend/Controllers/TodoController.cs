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
        public async Task<IActionResult> GetTodos([FromQuery] string? filter = "all", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
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
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // 分頁處理
            var todos = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

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

            // 包裝回傳格式
            var result = new
            {
                items = todoModels,
                currentPage = page,
                pageSize = pageSize,
                totalItems = totalItems,
                totalPages = totalPages
            };

            return Ok(result);
        }

        // 更新完成狀態
        [Authorize]
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> UpdateTodoComplete(int id, [FromBody] UpdateCompleteModel model)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsCompleted = model.IsCompleted;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 新增 Todo
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoEditModel model)
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
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return Ok("Todo created successfully!");
        }

        // 編輯 Todo
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditTodo(int id, [FromBody] TodoEditModel model)
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

            // 找出該 Todo
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound("Todo not found.");
            }

            // 驗證是否為該使用者的 Todo
            if (todo.UserId != userId)
            {
                return Forbid("You are not authorized to edit this Todo.");
            }

            // 更新資料
            todo.Title = model.Title;
            todo.Description = model.Description;
            if (model.DueDate.HasValue)
            {
                todo.DueDate = model.DueDate.Value.ToUniversalTime();
            }

            await _context.SaveChangesAsync();

            return Ok("Todo updated successfully.");
        }

        // 取得要被編輯的 Todo
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditTodo(int id)
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (userId == 0)
            {
                return Unauthorized("User is not authenticated.");
            }

            // 找出該 Todo
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound("Todo not found.");
            }

            // 驗證是否為該使用者的 Todo
            if (todo.UserId != userId)
            {
                return Forbid("You are not authorized to view this Todo.");
            }

            // 將該 Todo 轉換成可編輯的資料模型
            var editTodoModel = new TodoEditModel
            {
                Title = todo.Title,
                Description = todo.Description,
                DueDate = todo.DueDate
            };

            return Ok(editTodoModel);
        }

        // 刪除 Todo
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (userId == 0)
            {
                return Unauthorized("User is not authenticated.");
            }

            // 找出 Todo
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound("Todo not found.");
            }

            // 確認是否為該使用者的 Todo
            if (todo.UserId != userId)
            {
                return Forbid("You are not authorized to delete this Todo.");
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return Ok("Todo deleted successfully.");
        }
    }
}
