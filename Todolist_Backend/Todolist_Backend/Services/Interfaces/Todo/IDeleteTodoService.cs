namespace Todolist_Backend.Services.Interfaces.Todo
{
    public interface IDeleteTodoService
    {
        Task<(bool Success, bool Forbidden, string Message)> DeleteTodoAsync(int userId, int todoId);
    }
}
