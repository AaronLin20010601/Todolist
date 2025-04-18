using Microsoft.AspNetCore.Mvc;
using Todolist_Backend.Models.DTOs.Reset;
using Todolist_Backend.Services.Interfaces.VerifyCode;
using Todolist_Backend.Services.Interfaces.Reset;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetController : BaseApiController
    {
        private readonly ISendResetVerifyService _sendResetVerifyService;
        private readonly IResetPasswordService _resetService;

        public ResetController(IResetPasswordService resetService, ISendResetVerifyService sendResetVerifyService)
        {
            _resetService = resetService;
            _sendResetVerifyService = sendResetVerifyService;
        }

        // 重設密碼流程第一步 輸入已註冊的Email，發送驗證碼
        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] EmailDTO model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            var result = await _sendResetVerifyService.SendVerificationCodeAsync(model.Email);
            return result.Success ? Ok(result.Message) : BadRequest(new { Message = result.Message });
        }

        // 重設密碼流程第二步 輸入驗證碼與新密碼，完成重設
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetDTO model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            var result = await _resetService.ResetPasswordAsync(model);
            return result.Success ? Ok(result.Message) : BadRequest(new { Message = result.Message });
        }
    }
}
