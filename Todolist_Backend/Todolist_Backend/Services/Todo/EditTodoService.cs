using Todolist_Backend.Models;
using Todolist_Backend.Models.DTOs.Todo;
using Todolist_Backend.Services.Interfaces.Todo;

namespace Todolist_Backend.Services.Todo
{
    public class EditTodoService : IEditTodoService
    {
        private readonly TodolistDbContext _context;

        public EditTodoService(TodolistDbContext context)
        {
            _context = context;
        }

        public async Task<EditStatusDTO> EditTodoAsync(int userId, int todoId, TodoEditDTO model)
        {
            // 找出該 Todo
            var todo = await _context.Todos.FindAsync(todoId);
            if (todo == null)
            {
                return new EditStatusDTO
                {
                    Status = EditTodoStatus.NotFound,
                    Message = "Todo not found."
                };
            }

            // 驗證是否為該使用者的 Todo
            if (todo.UserId != userId)
            {
                return new EditStatusDTO
                {
                    Status = EditTodoStatus.Forbidden,
                    Message = "You are not authorized to edit this Todo."
                };
            }

            // 檢查標題是否為空
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                return new EditStatusDTO
                {
                    Status = EditTodoStatus.BadRequest,
                    Message = "Title is required."
                };
            }

            // 檢查 DueDate 是否小於當前時間
            if (model.DueDate.HasValue && model.DueDate.Value < DateTime.UtcNow)
            {
                return new EditStatusDTO
                {
                    Status = EditTodoStatus.BadRequest,
                    Message = "Due date cannot be in the past."
                };
            }

            // 更新資料
            todo.Title = model.Title;
            todo.Description = model.Description;
            if (model.DueDate.HasValue)
            {
                todo.DueDate = model.DueDate.Value.ToUniversalTime();
            }

            await _context.SaveChangesAsync();
            return new EditStatusDTO
            {
                Status = EditTodoStatus.Success,
                Message = "Todo updated successfully."
            };
        }
    }
}
