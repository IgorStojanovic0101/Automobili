using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.DTOs.User;

namespace Template.Application.Abstraction
{
    public abstract class UserLoginValidator : AbstractValidator<UserLoginRequestDTO>
    {
        protected UserLoginValidator()
        {
            // You could add common validation rules in the base class if applicable
            // For example, validate the common fields here if needed
        }
    }
}
