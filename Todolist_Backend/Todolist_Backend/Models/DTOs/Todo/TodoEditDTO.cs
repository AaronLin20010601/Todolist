using System.ComponentModel.DataAnnotations;

namespace Todolist_Backend.Models.DTOs.Todo
{
    // 編輯待辦事項
    public class TodoEditDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters.")]
        public string Title { get; set; } = null!; // 標題
        [StringLength(2000, ErrorMessage = "Description must be shorter than 2000 characters.")]
        public string? Description { get; set; } // 描述
        [Required(ErrorMessage = "Due date is required.")]
        public DateTime? DueDate { get; set; } // 截止時間
    }
}
