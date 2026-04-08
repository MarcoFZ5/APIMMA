namespace APIMMA.Exceptions
{
    public class UsernameAlreadyExistsException : ConflictException
    {
        public UsernameAlreadyExistsException(string username) : base($"Username '{username}' already exists.")
        {
        }
    }
}
