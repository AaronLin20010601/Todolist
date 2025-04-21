using Todolist_Backend.Models;
using Todolist_Backend.Services.Interfaces.Todo;

namespace Todolist_Backend.Services.Implements.Todo
{
    public class UpdateCompleteService : IUpdateCompleteService
    {
        private readonly TodolistDbContext _context;

        public UpdateCompleteService(TodolistDbContext context)
        {
            _context = context;
        }

        // 更新 Todo 完成狀態
        public async Task<bool> UpdateCompleteAsync(int todoId, bool isCompleted)
        {
            var todo = await _context.Todos.FindAsync(todoId);
            if (todo == null)
            {
                return false;
            }

            todo.IsCompleted = isCompleted;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
