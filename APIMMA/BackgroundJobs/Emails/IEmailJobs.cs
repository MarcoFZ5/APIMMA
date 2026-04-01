namespace APIMMA.BackgroundJobs.Emails
{
    public interface IEmailJobs
    {
        public Task sendConfirmationEmail(string to);
    }
}
