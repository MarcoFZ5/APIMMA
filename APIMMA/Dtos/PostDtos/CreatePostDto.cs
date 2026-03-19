namespace APIMMA.Dtos.PostDtos
{
    public class CreatePostDto
    {
        public string Content { get; set; }
        public string? Type { get; set; } // MANUAL | TRAININGLOG | CHALLENGE
        public string? ReferenceId { get; set; } // For referencing a training log or challenge if the post is of type TRAININGLOG or CHALLENGE ELSE NULL
    }
}
