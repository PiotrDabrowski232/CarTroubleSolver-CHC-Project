using AutoMapper;
using CarTroubleSolver.Logic.Dto.Accident;
using FluentValidation;

namespace CarTroubleSolver.Logic.Validation
{
    public class WebAccidentRequestDtoValidator : AbstractValidator<WebAccidentRequestDto>
    {
        public WebAccidentRequestDtoValidator()
        {
            RuleFor(a => a.AccidentDescription)
                .NotEmpty()
                .WithMessage("Fill description field")
                .NotNull()
                .WithMessage("Fill description field")
                .MinimumLength(30)
                .WithMessage("Your Description Should Be Longer Than 30 Letters");

            RuleFor(a => a.SelectedCar)
                .NotEmpty()
                .WithMessage("Select Car field")
                .NotNull()
                .WithMessage("Select Car field");

            RuleFor(a => a.CollisionSeverity)
                .NotEmpty()
                .WithMessage("Select Colision Severity field")
                .NotNull()
                .WithMessage("Select Colision Severity field");
        }

    }
}
