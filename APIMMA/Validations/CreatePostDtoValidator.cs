using APIMMA.Dtos.PostDtos;
using FluentValidation;

namespace APIMMA.Validations
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(post => post.Content)
                .NotEmpty(); 

        }
    }
}
