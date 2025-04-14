using Todolist_Backend.Models;
using Todolist_Backend.Models.Entities;
using Todolist_Backend.Services.Interfaces.Email;

namespace Todolist_Backend.Services.Email
{
    public class EmailLogger : IEmailLogger
    {
        private readonly TodolistDbContext _context;

        public EmailLogger(TodolistDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(IEnumerable<string> toEmails, string subject, string body, bool isSuccess)
        {
            // 將郵件記錄存入資料庫
            var emailLog = new EmailLog
            {
                ToEmail = string.Join(",", toEmails),
                Subject = subject,
                Body = body,
                IsSuccess = isSuccess,
                SentAt = DateTime.UtcNow
            };

            _context.EmailLogs.Add(emailLog);
            await _context.SaveChangesAsync();
        }
    }
}
