using FluentValidation;
using WebApiTutorial250818.WebApi.DTOs;

public class StudentPatchDtoValidator : AbstractValidator<StudentPatchDto>
{
    public StudentPatchDtoValidator()
    {
        // For PATCH, you normally allow nulls, but still validate if a value is provided
        When(x => x.FirstName != null, () =>
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name may not be empty when provided.")
                .MaximumLength(100);
        });

        When(x => x.LastName != null, () =>
        {
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name may not be empty when provided.")
                .MaximumLength(100);
        });

        When(x => x.Email != null, () =>
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .MaximumLength(255);
        });

        When(x => x.BirthDate != null, () =>
        {
            RuleFor(x => x.BirthDate!.Value)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("BirthDate cannot be in the future.");
        });
    }
}
