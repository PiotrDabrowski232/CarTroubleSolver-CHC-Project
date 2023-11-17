using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Logic.Dto.Cars;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Validation
{
    public class CarDtoValidator : AbstractValidator<CarDto>
    {
        public CarDtoValidator()
        {
            RuleFor(x => x.CarModels)
                .NotEmpty()
                .WithMessage("Fill CarModels Input");

            RuleFor(x => x.EngineType)
                .NotEmpty()
                .WithMessage("Fill EngineType Input");

            RuleFor(x => x.DoorCount)
                .NotEmpty()
                .WithMessage("Fill EngineType Input");

            RuleFor(x => x.Mileage)
               .NotEmpty()
               .WithMessage("Fill EngineType Input");

            RuleFor(x => x.Color)
               .NotEmpty()
               .WithMessage("Fill EngineType Input");
        }

        private bool BeValidInt(int value)
        {
            return value is int;
        }
    }
}
    