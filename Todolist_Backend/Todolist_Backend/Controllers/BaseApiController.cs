using Microsoft.AspNetCore.Mvc;

namespace Todolist_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IActionResult ModelStateErrorResponse()
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new { Message = string.Join(", ", errors) });
        }
    }
}
