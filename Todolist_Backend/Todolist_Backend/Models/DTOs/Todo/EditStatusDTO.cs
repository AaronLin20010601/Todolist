namespace Todolist_Backend.Models.DTOs.Todo
{
    // 編輯待辦事項提示訊息
    public class EditStatusDTO
    {
        public EditTodoStatus Status { get; set; } // http 狀態
        public string Message { get; set; } = ""; // 回傳訊息
    }

    public enum EditTodoStatus
    {
        Success,
        NotFound,
        Forbidden,
        BadRequest
    }
}
