using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todolist_Backend.Models.DTOs.Account;
using Todolist_Backend.Services.Interfaces.Account;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IGetAccountService _getAccountService;
        private readonly IUpdateAccountService _updateAccountService;
        private readonly IDeleteAccountService _deleteAccountService;

        public AccountController(IGetAccountService getAccountService, IUpdateAccountService updateAccountService, IDeleteAccountService deleteAccountService)
        {
            _getAccountService = getAccountService;
            _updateAccountService = updateAccountService;
            _deleteAccountService = deleteAccountService;
        }

        // 取得帳號可編輯資料
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAccount()
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized("User is not authenticated.");
            }

            // 找出用戶資料
            var account = await _getAccountService.GetAccountAsync(userId);
            return account == null ? NotFound("User not found.") : Ok(account); ;
        }

        // 編輯帳號資料
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountDTO model)
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0) 
            { 
                return Unauthorized("User is not authenticated."); 
            }

            var result = await _updateAccountService.UpdateAccountAsync(userId, model);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message); ;
        }

        // 刪除帳號
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccount()
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0) 
            { 
                return Unauthorized("User is not authenticated."); 
            }

            var result = await _deleteAccountService.DeleteAccountAsync(userId);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message); ;
        }
    }
}
