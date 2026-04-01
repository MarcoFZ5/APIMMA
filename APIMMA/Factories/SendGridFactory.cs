using SendGrid;

namespace APIMMA.Factories
{
    public class SendGridFactory
    {
        private readonly string _apiKey;

        public SendGridFactory(IConfiguration configuration)
        {
            _apiKey = configuration["SendGrid:ApiKey"];
        }
        
        public SendGridClient CreateClient()
        {
            return new SendGridClient(_apiKey);
        }
    }
}
