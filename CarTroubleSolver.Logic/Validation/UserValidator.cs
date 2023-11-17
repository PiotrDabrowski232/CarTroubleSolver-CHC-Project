using CarTroubleSolver.Data.Database;
using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Logic.Dto.User;
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
                .NotEmpty()
                .WithMessage("Fill DateOfBirth Input")
                .Must(BeValidDate)
                .WithMessage("Date of birth should be valid date dd-mm-yyyy.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Fill PhoneNumber Input")
                .Must(BeValidInt)
                .WithMessage("Phone Number Should Have only numers");
        }

        private bool BeValidDate(DateTime date)
        {
            return date is DateTime;
        }

        private bool BeValidInt(int value)
        {
            return value is int;
        }
    }
}
