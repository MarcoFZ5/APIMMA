namespace APIMMA.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid UserId) : base($"User with id {UserId} not found")
        {
        }
    }
}
