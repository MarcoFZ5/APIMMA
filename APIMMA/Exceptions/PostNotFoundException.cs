namespace APIMMA.Exceptions
{
    public class PostNotFoundException  : NotFoundException 
    {
        public PostNotFoundException(int postId) : base($"Post with id {postId} not found.")
        {
        }
    }
}
