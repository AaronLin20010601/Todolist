using System.ComponentModel.DataAnnotations;

namespace Todolist_Backend.Models.DTOs.Verification
{
    public class EmailDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = null!; // email
    }
}
