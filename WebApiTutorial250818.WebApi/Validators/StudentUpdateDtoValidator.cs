using FluentValidation;
using WebApiTutorial250818.WebApi.DTOs;

namespace WebApiTutorial250818.WebApi.Validators
{
    public class StudentUpdateDtoValidator : AbstractValidator<StudentUpdateDto>
    {
        public StudentUpdateDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

            RuleFor(x => x.PersonalEmail)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.PersonalEmail))
                .MaximumLength(255);
        }
    }
}
