using Todolist_Backend.Models;
using Todolist_Backend.Services.Interfaces.Todo;

namespace Todolist_Backend.Services.Implements.Todo
{
    public class DeleteTodoService : IDeleteTodoService
    {
        private readonly TodolistDbContext _context;

        public DeleteTodoService(TodolistDbContext context)
        {
            _context = context;
        }

        // 刪除現有的 Todo
        public async Task<(bool Success, bool Forbidden, string Message)> DeleteTodoAsync(int userId, int todoId)
        {
            // 找出 Todo
            var todo = await _context.Todos.FindAsync(todoId);
            if (todo == null)
            {
                return (false, false, "Todo not found.");
            }

            // 確認是否為該使用者的 Todo
            if (todo.UserId != userId)
            {
                return (false, true, "You are not authorized to delete this Todo.");
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return (true, false, "Todo deleted successfully.");
        }
    }
}
