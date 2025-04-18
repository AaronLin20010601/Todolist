using System.ComponentModel.DataAnnotations;

namespace Todolist_Backend.Models.DTOs.Reset
{
    public class ResetDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = null!; // 用戶的 Email
        [Required(ErrorMessage = "Verification code is required.")]
        public string VerificationCode { get; set; } = null!; // 用戶輸入的驗證碼
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 30 characters.")]
        public string Password { get; set; } = null!; // 密碼
        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; } = null!; // 確認密碼
    }
}
