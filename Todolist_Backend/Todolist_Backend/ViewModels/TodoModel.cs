namespace Todolist_Backend.ViewModels
{
    public class TodoModel
    {
        public int Id { get; set; } // 主鍵
        public string Title { get; set; } = null!; // 標題
        public string? Description { get; set; } // 描述
        public string Status { get; set; } = null!; // 狀態
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 建立時間
        public DateTime? DueDate { get; set; } // 截止時間
    }
}
