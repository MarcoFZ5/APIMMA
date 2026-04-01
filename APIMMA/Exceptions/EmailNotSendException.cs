namespace APIMMA.Exceptions
{
    public class EmailNotSendException : Exception
    {
        public EmailNotSendException(string message) : base(message)
        {
        }
    }
}
