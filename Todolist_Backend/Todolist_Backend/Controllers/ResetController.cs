using Microsoft.AspNetCore.Mvc;
using Todolist_Backend.Models.DTOs.Reset;
using Todolist_Backend.Services.Interfaces.Reset;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetController : BaseApiController
    {
        private readonly IResetPasswordService _resetService;

        public ResetController(IResetPasswordService resetService)
        {
            _resetService = resetService;
        }

        // 重設密碼
        [HttpPost]
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
