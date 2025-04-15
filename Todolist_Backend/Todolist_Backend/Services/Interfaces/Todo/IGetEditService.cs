using Todolist_Backend.Models.DTOs.Todo;

namespace Todolist_Backend.Services.Interfaces.Todo
{
    public interface IGetEditService
    {
        Task<(bool Success, bool Forbidden, string Message, TodoEditDTO? Data)> GetEditAsync(int userId, int todoId);
    }
}
