using APIMMA.Dtos.AuthDtos;
using FluentValidation;
using System.Data;

namespace APIMMA.Validations
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator() 
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(user => user.Password)
                .NotEmpty()
                .MinimumLength(6);
        }

    }
}
