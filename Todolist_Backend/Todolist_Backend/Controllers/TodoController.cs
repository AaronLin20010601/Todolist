using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todolist_Backend.Models.DTOs.Todo;
using Todolist_Backend.Services.Interfaces.Todo;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : BaseApiController
    {
        private readonly IGetTodosService _getTodosService;
        private readonly IUpdateCompleteService _updateCompleteService;
        private readonly ICreateTodoService _createTodoService;
        private readonly IGetEditService _getEditService;
        private readonly IEditTodoService _editTodoService;
        private readonly IDeleteTodoService _deleteTodoService;

        public TodoController(
            IGetTodosService getTodosService, 
            IUpdateCompleteService updateCompleteService, 
            ICreateTodoService createTodoService, 
            IGetEditService getEditService, 
            IEditTodoService editTodoService, 
            IDeleteTodoService deleteTodoService)
        {
            _getTodosService = getTodosService;
            _updateCompleteService = updateCompleteService;
            _createTodoService = createTodoService;
            _getEditService = getEditService;
            _editTodoService = editTodoService;
            _deleteTodoService = deleteTodoService;
        }

        // 取得登入使用者的 Todo 列表
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] string? filter = "all", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var data = await _getTodosService.GetTodosAsync(userId, filter, page, pageSize);
            return Ok(data);
        }

        // 更新完成狀態
        [Authorize]
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> UpdateTodoComplete(int id, [FromBody] UpdateCompleteDTO model)
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var success = await _updateCompleteService.UpdateCompleteAsync(id, model.IsCompleted);
            return success ? NoContent() : NotFound();
        }

        // 新增 Todo
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoEditDTO model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var result = await _createTodoService.CreateTodoAsync(userId, model);
            return result.Success ? Ok(result.Message) : BadRequest(new { Message = result.Message });
        }

        // 取得要被編輯的 Todo
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditTodo(int id)
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var result = await _getEditService.GetEditAsync(userId, id);
            return result.Success ? Ok(result.Data) : (result.Forbidden ? Forbid(result.Message) : NotFound(new { Message = result.Message }));
        }

        // 編輯 Todo
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditTodo(int id, [FromBody] TodoEditDTO model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var result = await _editTodoService.EditTodoAsync(userId, id, model);
            return result switch
            {
                { Status: EditTodoStatus.NotFound } => NotFound(new { Message = result.Message }),
                { Status: EditTodoStatus.Forbidden } => Forbid(result.Message),
                { Status: EditTodoStatus.BadRequest } => BadRequest(new { Message = result.Message }),
                _ => Ok(result.Message)
            };
        }

        // 刪除 Todo
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var result = await _deleteTodoService.DeleteTodoAsync(userId, id);
            return result.Success ? Ok(result.Message) : (result.Forbidden ? Forbid(result.Message) : NotFound(new { Message = result.Message }));
        }
    }
}
