using Todolist_Backend.Models.DTOs.Reset;

namespace Todolist_Backend.Services.Interfaces.Reset
{
    public interface IResetPasswordService
    {
        Task<(bool Success, string Message)> ResetPasswordAsync(ResetDTO model);
    }
}
