using FluentValidation;
using APIMMA.Dtos.ChallengeDtos;

namespace APIMMA.Validations
{
    public class ChallengeDtoValidator : AbstractValidator<ChallengeDto>
    {
        public ChallengeDtoValidator()
        {
            RuleFor(challenge => challenge.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(challenge => challenge.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(challenge => challenge.StartDate)
                .LessThan(challenge => challenge.EndDate).WithMessage("Start date must be before end date.");

            RuleFor(challenge => challenge.EndDate)
                .LessThan(DateTime.Now).WithMessage("End date must be in the future.");
        }
    }
}
