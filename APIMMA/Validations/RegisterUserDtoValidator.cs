using APIMMA.Dtos;
using FluentValidation;

namespace APIMMA.Validations 
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty();

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(user => user.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
