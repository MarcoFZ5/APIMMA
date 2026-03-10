namespace APIMMA.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(int UserId) : base($"User with id {UserId} not found")
        {
        }
    }
}
