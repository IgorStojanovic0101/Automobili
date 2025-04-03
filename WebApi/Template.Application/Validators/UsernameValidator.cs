using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction;

namespace Template.Application.Validators
{
    public class UsernameValidator : UserLoginValidator
    {
        public UsernameValidator()
        {
            // This is specific to the username validation
            RuleFor(dto => dto.Username)
                .Must(value => value == "Igor" || value == "Ema")
                .WithMessage("Invalid username.");
        }
    }
}
