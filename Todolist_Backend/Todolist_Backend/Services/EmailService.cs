using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Todolist_Backend.Models;
using Todolist_Backend.Models.Entities;
using Todolist_Backend.Settings;

namespace Todolist_Backend.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(IEnumerable<string> toEmails, string subject, string body);
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

        public async Task<bool> SendEmailAsync(IEnumerable<string> toEmails, string subject, string body)
        {
            var client = new MailjetClient(_settings.ApiKey, _settings.ApiSecret);

            // 動態構建收件人清單
            var recipients = new JArray();
            foreach (var toEmail in toEmails)
            {
                recipients.Add(new JObject { { "Email", toEmail } });
            }

            var request = new MailjetRequest
            {
                Resource = Send.Resource
            }
            .Property(Send.FromEmail, _settings.SenderEmail)
            .Property(Send.FromName, "Todolist")
            .Property(Send.Subject, subject)
            .Property(Send.TextPart, body)
            .Property(Send.HtmlPart, $"<p>{body}</p>")
            .Property(Send.Recipients, recipients);

            // 將郵件記錄存入資料庫
            var emailLog = new EmailLog
            {
                ToEmail = string.Join(",", toEmails),
                Subject = subject,
                Body = body,
                SentAt = DateTime.UtcNow,
            };

            var response = await client.PostAsync(request);
            bool success = response.IsSuccessStatusCode;
            if (!success)
            {
                Console.WriteLine($"Mailjet Error: {response.StatusCode} - {response.GetErrorMessage()}");
                Console.WriteLine($"Mailjet Raw Response: {response.GetData()}");
                Console.WriteLine($"SenderEmail from settings: {_settings.SenderEmail}");
            }
            emailLog.IsSuccess = success;

            _context.EmailLogs.Add(emailLog);
            await _context.SaveChangesAsync();

            return success;
        }
    }
}
