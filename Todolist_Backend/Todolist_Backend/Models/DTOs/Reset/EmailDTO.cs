using System.ComponentModel.DataAnnotations;

namespace Todolist_Backend.Models.DTOs.Reset
{
    public class EmailDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = null!; // email
    }
}
