using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.DTOs.User;
using Template.Application.Validators;


namespace Test.Application.Validators
{
    public class UserLoginDTOValidator : AbstractValidator<UserLoginRequestDTO>
    {
        public UserLoginDTOValidator(UsernameValidator usernameValidator, PasswordValidator passwordValidator)
        {
            // You can combine specific validation rules like this:
            Include(usernameValidator);
            Include(passwordValidator);
        }
    }
}
