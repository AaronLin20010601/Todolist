using Todolist_Backend.Services.Interfaces.Token;

namespace Todolist_Backend.Services.Token
{
    public class VerificationCodeService : IVerificationCodeService
    {
        // 生成隨機的驗證碼
        public string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
