namespace Todolist_Backend.Services
{
    public class TokenService
    {
        // 生成隨機的驗證碼
        public string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString(); // 生成6位數字
        }
    }
}
