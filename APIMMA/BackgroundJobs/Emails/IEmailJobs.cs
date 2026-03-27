namespace APIMMA.BackgroundJobs.Emails
{
    public interface IEmailJobs
    {
        public Task sendEmail(string to, string subject, string body);
    }
}
