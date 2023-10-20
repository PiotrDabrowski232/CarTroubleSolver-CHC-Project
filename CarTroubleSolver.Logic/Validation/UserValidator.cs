using CarTroubleSolver.Data.Database;
using CarTroubleSolver.Logic.Dto;
using FluentValidation;

namespace CarTroubleSolver.Logic.Validation
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Fill Name Input");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Fill Surname Input");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Fill Password Input")
                .MinimumLength(8).WithMessage("Password should be longer than 8 characters")
                .Matches("[A-Z]").WithMessage("Password should contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password should contain at least one lowercase letter")
                .Matches("[!@#$%^&*()_+{}|:;<>,.?~]").WithMessage("Password should contain at least one special character");

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password)
                .WithMessage("Both passwords should be the same");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email Input Was Empty")
                .EmailAddress()
                .WithMessage("Invalid email format. Please enter a valid email address.");

            RuleFor(x => x.DateOfBirth)
                .Must(BeValidDate)
                .WithMessage("Data urodzenia musi być prawidłową datą.");
        }
        private bool BeValidDate(DateTime date)
        {
            return date is DateTime;
        }
    }
}
