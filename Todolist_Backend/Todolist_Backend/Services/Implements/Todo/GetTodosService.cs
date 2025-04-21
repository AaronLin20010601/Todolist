using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.Models.DTOs.Todo;
using Todolist_Backend.Services.Interfaces.Todo;

namespace Todolist_Backend.Services.Implements.Todo
{
    public class GetTodosService : IGetTodosService
    {
        private readonly TodolistDbContext _context;

        public GetTodosService(TodolistDbContext context)
        {
            _context = context;
        }

        // 取得使用者 Todo 資料
        public async Task<object> GetTodosAsync(int userId, string? filter, int page, int pageSize)
        {
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
            var todoModels = todos.Select(t => new TodoDTO
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.IsCompleted ? "completed" : t.DueDate < DateTime.UtcNow ? "expired" : "incomplete",
                CreatedAt = t.CreatedAt,
                DueDate = t.DueDate
            }).ToList();

            // 包裝回傳格式
            var result = new
            {
                items = todoModels,
                currentPage = page,
                pageSize,
                totalItems,
                totalPages
            };

            return result;
        }
    }
}
