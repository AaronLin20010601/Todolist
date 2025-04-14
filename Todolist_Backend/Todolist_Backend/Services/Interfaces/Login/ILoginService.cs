using Todolist_Backend.Models.Entities;
using Todolist_Backend.Models.DTOs.Login;

namespace Todolist_Backend.Services.Interfaces.Login
{
    public interface ILoginService
    {
        Task<(bool Success, string Message, string? Token, User? User)> LoginAsync(LoginDTO model);
    }
}
