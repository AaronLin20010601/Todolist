namespace Todolist_Backend.Services.Interfaces.Todo
{
    public interface IGetTodosService
    {
        Task<object> GetTodosAsync(int userId, string? filter, int page, int pageSize);
    }
}
