namespace Todolist_Backend.Services.Interfaces.VerifyCode
{
    public interface ISendRegisterVerifyService
    {
        Task<(bool Success, string Message)> SendVerificationCodeAsync(string email);
    }
}
