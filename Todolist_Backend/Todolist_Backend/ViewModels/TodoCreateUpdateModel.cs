namespace Todolist_Backend.ViewModels
{
    public class TodoCreateUpdateModel
    {
        public string Title { get; set; } = null!; // 標題
        public string? Description { get; set; } // 描述
        public bool IsCompleted { get; set; } = false; // 是否完成
        public DateTime? DueDate { get; set; } // 截止時間
    }
}
