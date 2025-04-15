namespace Todolist_Backend.Services.Interfaces.Todo
{
    public interface IUpdateCompleteService
    {
        Task<bool> UpdateCompleteAsync(int todoId, bool isCompleted);
    }
}
