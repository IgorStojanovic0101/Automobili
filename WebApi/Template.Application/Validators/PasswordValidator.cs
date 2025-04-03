using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction;

namespace Template.Application.Validators
{
    public class PasswordValidator : UserLoginValidator
    {
        public PasswordValidator()
        {
            // Add password-specific rules if needed
            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Password cannot be empty.");
        }
    }
}
