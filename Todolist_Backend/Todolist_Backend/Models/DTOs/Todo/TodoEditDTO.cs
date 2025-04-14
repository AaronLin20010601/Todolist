namespace Todolist_Backend.Models.DTOs.Todo
{
    public class TodoEditDTO
    {
        public string Title { get; set; } = null!; // 標題
        public string? Description { get; set; } // 描述
        public DateTime? DueDate { get; set; } // 截止時間
    }
}
