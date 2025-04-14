using Todolist_Backend.Models.DTOs.Register;

namespace Todolist_Backend.Services.Interfaces.Register
{
    public interface IRegisterService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterDTO model);
    }
}
