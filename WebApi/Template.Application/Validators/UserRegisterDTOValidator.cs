﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.DTOs.User;

namespace Test.Application.Validators
{
    public class UserRegisterDTOValidator : AbstractValidator<UserRegisterRequestDTO>
    {
        public UserRegisterDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");
               

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                 .WithMessage("Invalid email format.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
              

        }
    }
}
