using Todolist_Backend.Models;
using Todolist_Backend.Models.DTOs.Account;
using Todolist_Backend.Services.Interfaces.Account;

namespace Todolist_Backend.Services.Implements.Account
{
    public class UpdateAccountService : IUpdateAccountService
    {
        private readonly TodolistDbContext _context;

        public UpdateAccountService(TodolistDbContext context)
        {
            _context = context;
        }

        // 更新帳號資料
        public async Task<(bool Success, string Message)> UpdateAccountAsync(int userId, AccountDTO model)
        {
            // 找出用戶資料
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            user.Username = model.Username;
            await _context.SaveChangesAsync();
            return (true, "Username updated successfully.");
        }
    }
}
