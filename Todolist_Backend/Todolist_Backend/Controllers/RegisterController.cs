using Microsoft.AspNetCore.Mvc;
using Todolist_Backend.Models.DTOs.Register;
using Todolist_Backend.Services.Interfaces.VerifyCode;
using Todolist_Backend.Services.Interfaces.Register;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ISendRegisterVerifyService _sendRegisterVerifyService;
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService, ISendRegisterVerifyService sendRegisterVerifyService)
        {
            _registerService = registerService;
            _sendRegisterVerifyService = sendRegisterVerifyService;
        }

        // 註冊流程的第一步 發送驗證碼到Email
        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] EmailDTO model)
        {
            var result = await _sendRegisterVerifyService.SendVerificationCodeAsync(model.Email);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        // 註冊流程的第二步 驗證驗證碼，並註冊用戶
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _registerService.RegisterAsync(model);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }
    }
}
