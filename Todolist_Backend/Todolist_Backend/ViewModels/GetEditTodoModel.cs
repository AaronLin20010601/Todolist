namespace Todolist_Backend.ViewModels
{
    public class GetEditTodoModel
    {
        public string Title { get; set; } = null!; // 標題
        public string? Description { get; set; } // 描述
        public DateTime? DueDate { get; set; } // 截止時間
    }
}
