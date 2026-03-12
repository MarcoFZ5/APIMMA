using APIMMA.Dtos.PostDtos;
using FluentValidation;

namespace APIMMA.Validations
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(post => post.Title)
                .NotEmpty();

            RuleFor(post => post.Content)
                .NotEmpty(); 

        }
    }
}
