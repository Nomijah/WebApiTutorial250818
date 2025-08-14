using FluentValidation;
using WebApiTutorial250818.WebApi.DTOs;

public class StudentUpdateDtoValidator : AbstractValidator<StudentUpdateDto>
{
    public StudentUpdateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
            .MaximumLength(255);
    }
}