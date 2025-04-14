using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Todolist_Backend.Settings;
using Todolist_Backend.Services.Interfaces.Email;

namespace Todolist_Backend.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly MailjetSettings _settings;

        public EmailSender(IOptions<MailjetSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<bool> SendAsync(IEnumerable<string> toEmails, string subject, string htmlBody, string textBody)
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
            .Property(Send.FromName, _settings.SenderName)
            .Property(Send.Subject, subject)
            .Property(Send.TextPart, textBody)
            .Property(Send.HtmlPart, htmlBody)
            .Property(Send.Recipients, recipients);

            // 送出信件並回傳寄信狀態
            var response = await client.PostAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
