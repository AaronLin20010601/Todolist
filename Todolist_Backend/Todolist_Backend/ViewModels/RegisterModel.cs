namespace Todolist_Backend.ViewModels
{
    public class RegisterModel
    {
        public string Email { get; set; } = null!; // 用戶的 Email
        public string VerificationCode { get; set; } = null!; // 用戶輸入的驗證碼
        public string Username { get; set; } = null!; // 用戶名稱
        public string Password { get; set; } = null!; // 密碼
        public string ConfirmPassword { get; set; } = null!; // 確認密碼
    }
}
