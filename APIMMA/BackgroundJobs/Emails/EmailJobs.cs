using APIMMA.Data;
using Microsoft.EntityFrameworkCore;

namespace APIMMA.BackgroundJobs.Emails
{
    public class EmailJobs : IEmailJobs
    {
        public readonly ILogger _logger;
        public AppDbContext _context;
        
        public EmailJobs(ILogger<EmailJobs> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task sendEmail(string to, string subject, string body)
        {
            var exists = await _context.Users.AnyAsync(user => user.Email == to);

            if (exists)
            {
                _logger.LogInformation($"Email sent to {to} with subject: {subject} and body: {body}");
            }
            else
            {
                _logger.LogError("Failed to send email. User with email {to} does not exist.", to);
            }
        }
    }
}
