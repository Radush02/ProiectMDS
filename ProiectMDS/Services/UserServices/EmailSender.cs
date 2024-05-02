using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using ProiectMDS.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ProiectMDS.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly SendGridSettings _sendGridSettings;
        public EmailSender(ISendGridClient sendGridClient,IOptions<SendGridSettings> sendGridSettings)
        {
            _sendGridClient = sendGridClient;
            _sendGridSettings = sendGridSettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_sendGridSettings.SendGridEmail, _sendGridSettings.SendGridName),
                Subject = subject,
                PlainTextContent = htmlMessage,
            };
            msg.AddTo(new EmailAddress(email));
            await _sendGridClient.SendEmailAsync(msg);
        }
    }
}
