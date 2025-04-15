using Todolist_Backend.Models.DTOs.Todo;

namespace Todolist_Backend.Services.Interfaces.Todo
{
    public interface ICreateTodoService
    {
        Task<(bool Success, string Message)> CreateTodoAsync(int userId, TodoEditDTO model);
    }
}
