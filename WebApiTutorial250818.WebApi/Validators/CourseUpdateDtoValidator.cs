using FluentValidation;
using WebApiTutorial250818.WebApi.DTOs;

namespace WebApiTutorial250818.WebApi.Validators
{
    public class CourseUpdateDtoValidator : AbstractValidator<CourseUpdateDto>
    {
        public CourseUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.Credits)
                .InclusiveBetween(0, 50).WithMessage("Credits must be between 0 and 50.");
        }
    }
}
