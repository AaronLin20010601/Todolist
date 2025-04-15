namespace Todolist_Backend.Services.Interfaces.Account
{
    public interface IDeleteAccountService
    {
        Task<(bool Success, string Message)> DeleteAccountAsync(int userId);
    }
}
