namespace Todolist_Backend.Services.Interfaces.Todo
{
    public interface IGetTodosService
    {
        Task<object> GetTodosAsync(int userId, string? status, DateTime? startDueDate, DateTime? endDueDate, int page, int pageSize);
    }
}
