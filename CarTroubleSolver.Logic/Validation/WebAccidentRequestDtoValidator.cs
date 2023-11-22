﻿using AutoMapper;
using CarTroubleSolver.Logic.Dto.Accident;
using FluentValidation;

namespace CarTroubleSolver.Logic.Validation
{
    public class WebAccidentRequestDtoValidator : AbstractValidator<WebAccidentRequestDto>
    {
        public WebAccidentRequestDtoValidator()
        {
            RuleFor(a => a.AccidentDescription)
                .MinimumLength(30)
                .WithMessage("Your Description Should Be Longer Than 30 Letters");
        }

    }
}