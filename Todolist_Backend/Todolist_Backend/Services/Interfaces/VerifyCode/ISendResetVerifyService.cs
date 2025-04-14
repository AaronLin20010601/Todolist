namespace Todolist_Backend.Services.Interfaces.VerifyCode
{
    public interface ISendResetVerifyService
    {
        Task<(bool Success, string Message)> SendVerificationCodeAsync(string email);
    }
}
