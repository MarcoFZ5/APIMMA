namespace APIMMA.Exceptions
{
    public class PostNotFoundException  : NotFoundException 
    {
        public PostNotFoundException(Guid postId) : base($"Post with id {postId} not found.")
        {
        }
    }
}
