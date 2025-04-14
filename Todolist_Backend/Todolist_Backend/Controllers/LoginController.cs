using Microsoft.AspNetCore.Mvc;
using Todolist_Backend.Models.DTOs.Login;
using Todolist_Backend.Services.Interfaces.Login;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var result = await _loginService.LoginAsync(model);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            // 登入成功
            return Ok(new
            {
                message = result.Message,
                token = result.Token,
                user = new
                {
                    result.User!.Id,
                    result.User.Username,
                    result.User.Email
                }
            });
        }
    }
}
