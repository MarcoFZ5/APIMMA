
using APIMMA.Exceptions;
using APIMMA.Factories;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace APIMMA.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridClient _client;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailService (SendGridFactory factory, IConfiguration configuration)
        {
            _client = factory.CreateClient();
            _fromEmail = "marcofuenteszumbadl@gmail.com";
            _fromName = "MMA COMMUNITY";
        }

        public async Task SendEmailAsync(string to, string subject, string plainText, string htmlContent)
        {
            var from = new EmailAddress(_fromEmail, _fromName);

            var toEmail = new EmailAddress(to);

            var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, plainText, htmlContent);

            var response = await _client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new EmailNotSendException($"Failed to send email to {to}. Status code: {response.StatusCode}");
            }
        }
    }
}
