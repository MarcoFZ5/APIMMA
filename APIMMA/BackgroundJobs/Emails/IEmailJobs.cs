namespace APIMMA.Services
{
    public interface IBackgroundJobs
    {
        public Task sendEmail(string to, string subject, string body);
    }
}
