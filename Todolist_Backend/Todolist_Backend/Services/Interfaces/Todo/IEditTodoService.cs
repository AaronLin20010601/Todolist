using Todolist_Backend.Models.DTOs.Todo;

namespace Todolist_Backend.Services.Interfaces.Todo
{
    public interface IEditTodoService
    {
        Task<EditStatusDTO> EditTodoAsync(int userId, int todoId, TodoEditDTO model);
    }
}
