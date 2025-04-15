using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.Models.Entities;
using Todolist_Backend.Models.DTOs.Todo;
using Todolist_Backend.Services.Interfaces.Todo;

namespace Todolist_Backend.Services.Todo
{
    public class GetEditService : IGetEditService
    {
        private readonly TodolistDbContext _context;

        public GetEditService(TodolistDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, bool Forbidden, string Message, TodoEditDTO? Data)> GetEditAsync(int userId, int todoId)
        {
            // 找出該 Todo
            var todo = await _context.Todos.FindAsync(todoId);
            if (todo == null)
            {
                return (false, false, "Todo not found.", null);
            }

            // 驗證是否為該使用者的 Todo
            if (todo.UserId != userId)
            {
                return (false, true, "You are not authorized to view this Todo.", null);
            }

            // 將該 Todo 轉換成可編輯的資料模型
            var editTodoModel = new TodoEditDTO
            {
                Title = todo.Title,
                Description = todo.Description,
                DueDate = todo.DueDate
            };

            return (true, false, "Success", editTodoModel);
        }
    }
}
