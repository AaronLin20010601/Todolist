using Todolist_Backend.Models;
using Todolist_Backend.Models.DTOs.Account;
using Todolist_Backend.Services.Interfaces.Account;

namespace Todolist_Backend.Services.Account
{
    public class GetAccountService : IGetAccountService
    {
        private readonly TodolistDbContext _context;

        public GetAccountService(TodolistDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDTO?> GetAccountAsync(int userId)
        {
            // 找出用戶資料
            var user = await _context.Users.FindAsync(userId);
            if (user == null) 
            {
                return null;
            }

            return new AccountDTO
            {
                Username = user.Username
            };
        }
    }
}
