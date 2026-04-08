namespace APIMMA.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string plainText, string htmlContent);
    }
}
