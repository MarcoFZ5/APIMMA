using APIMMA.Data;
using APIMMA.Services;
using Microsoft.EntityFrameworkCore;

namespace APIMMA.BackgroundJobs.Emails
{
    public class EmailJobs : IEmailJobs
    {
        private readonly IEmailService _email;

        public EmailJobs(IEmailService email)
        {
            _email = email;
        }

        public async Task sendConfirmationEmail(string to)
        {
            var message = $"Confirmation Email for the MMA COMMUNITY APP";
            var subject = "MMA COMMUNITY APP - Confirmation Email";

            var htmlContext = $"<p> {message} </p>";

            await _email.SendEmailAsync(to, subject, message, htmlContext);
        }
    }
}
