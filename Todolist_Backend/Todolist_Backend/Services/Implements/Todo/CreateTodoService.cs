using Todolist_Backend.Models;
using Todolist_Backend.Models.DTOs.Todo;
using Todolist_Backend.Services.Interfaces.Todo;

namespace Todolist_Backend.Services.Implements.Todo
{
    public class CreateTodoService : ICreateTodoService
    {
        private readonly TodolistDbContext _context;

        public CreateTodoService(TodolistDbContext context)
        {
            _context = context;
        }

        // 建立新的 Todo
        public async Task<(bool Success, string Message)> CreateTodoAsync(int userId, TodoEditDTO model)
        {
            // 檢查 DueDate 是否小於當前時間
            if (model.DueDate.HasValue && model.DueDate.Value < DateTime.UtcNow)
            {
                return (false, "Due date cannot be in the past.");
            }

            // 建立 Todo
            var todo = new Models.Entities.Todo
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
            return (true, "Todo created successfully!");
        }
    }
}
