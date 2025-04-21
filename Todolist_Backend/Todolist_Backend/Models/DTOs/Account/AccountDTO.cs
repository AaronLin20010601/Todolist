using System.ComponentModel.DataAnnotations;

namespace Todolist_Backend.Models.DTOs.Account
{
    // 取得和編輯用戶資料
    public class AccountDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 20 characters.")]
        public string Username { get; set; } = null!; // 用戶名
    }
}
