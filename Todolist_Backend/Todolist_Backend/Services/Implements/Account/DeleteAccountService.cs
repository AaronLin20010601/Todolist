using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.Services.Interfaces.Account;

namespace Todolist_Backend.Services.Implements.Account
{
    public class DeleteAccountService : IDeleteAccountService
    {
        private readonly TodolistDbContext _context;

        public DeleteAccountService(TodolistDbContext context)
        {
            _context = context;
        }

        // 刪除帳號和屬於該帳號的 Todos
        public async Task<(bool Success, string Message)> DeleteAccountAsync(int userId)
        {
            // 找出用戶資料
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            // 取得使用者的所有 Todos
            var userTodos = await _context.Todos
                .Where(t => t.UserId == userId)
                .ToListAsync();

            // 刪除 todos
            _context.Todos.RemoveRange(userTodos);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return (true, "Account deleted successfully.");
        }
    }
}
