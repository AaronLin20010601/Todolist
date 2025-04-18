using System.ComponentModel.DataAnnotations;

namespace Todolist_Backend.Models.DTOs.Login
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = null!; // email
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!; // 密碼
    }
}
