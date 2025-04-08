using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Todolist_Backend.Models;
using Todolist_Backend.Settings;

namespace Todolist_Backend.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly MailjetSettings _settings;
        private readonly TodolistDbContext _context;

        public EmailService(IOptions<MailjetSettings> options, TodolistDbContext context)
        {
            _settings = options.Value;
            _context = context;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            MailjetClient client = new MailjetClient(_settings.ApiKey, _settings.ApiSecret);

            var message = new
            {
                From = new
                {
                    Email = _settings.SenderEmail,
                    Name = "TodoList"
                },
                To = new[]
                {
                    new
                    {
                        Email = toEmail,
                        Name = toEmail
                    }
                },
                Subject = subject,
                TextPart = body,
                HTMLPart = $"<p>{body}</p>"
            };

            var request = new MailjetRequest
            {
                Resource = Send.Resource
            }
            .Property(Send.Messages, new JArray
            {
                JObject.FromObject(message)
            });

            var response = await client.PostAsync(request);
            bool success = response.IsSuccessStatusCode;

            if (!success)
            {
                Console.WriteLine($"Mailjet Error: {response.StatusCode} - {response.GetErrorMessage()}");
                Console.WriteLine($"Mailjet Raw Response: {response.GetData()}");
                Console.WriteLine($"SenderEmail from settings: {_settings.SenderEmail}");
                Console.WriteLine($"ApiKey: {_settings.ApiKey}");
            }

            // 將郵件記錄存入資料庫
            var emailLog = new EmailLog
            {
                ToEmail = toEmail,
                Subject = subject,
                Body = body,
                SentAt = DateTime.UtcNow,
                IsSuccess = success
            };

            _context.EmailLogs.Add(emailLog);
            await _context.SaveChangesAsync();

            return success;
        }
    }
}
