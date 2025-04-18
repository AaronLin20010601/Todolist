using System.ComponentModel.DataAnnotations;

namespace Todolist_Backend.Models.DTOs.Register
{
    public class EmailDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = null!; // email
    }
}
