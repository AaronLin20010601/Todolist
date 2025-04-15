using Todolist_Backend.Models.DTOs.Account;

namespace Todolist_Backend.Services.Interfaces.Account
{
    public interface IUpdateAccountService
    {
        Task<(bool Success, string Message)> UpdateAccountAsync(int userId, AccountDTO model);
    }
}
