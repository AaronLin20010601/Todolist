using Todolist_Backend.Models.DTOs.Account;

namespace Todolist_Backend.Services.Interfaces.Account
{
    public interface IGetAccountService
    {
        Task<AccountDTO?> GetAccountAsync(int userId);
    }
}
