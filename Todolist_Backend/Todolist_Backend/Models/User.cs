namespace Todolist_Backend.Models
{
    public class User
    {
        public int Id { get; set; } // 主鍵
        public string Username { get; set; } = null!; // 用戶名
        public string Email { get; set; } = null!; // email
        public string PasswordHash { get; set; } = null!; // 加密密碼
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 建立時間

        public ICollection<Todo> Todos { get; set; } = new List<Todo>();
    }
}
