namespace Todolist_Backend.Models
{
    public class Todo
    {
        public int Id { get; set; } // 主鍵
        public string Title { get; set; } = null!; // 標題
        public string? Description { get; set; } // 描述
        public bool IsCompleted { get; set; } = false; // 是否完成
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 建立時間
        public DateTime? DueDate { get; set; } // 截止時間

        public int UserId { get; set; } // 用戶編號
        public User User { get; set; } = null!;
    }
}
