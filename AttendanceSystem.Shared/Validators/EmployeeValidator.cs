using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Shared.DTOs;
using FluentValidation;

namespace AttendanceSystem.Shared.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
            .Matches(@"^[A-Za-z\s]*$").WithMessage("'{PropertyName}' should only contain letters.")
            .Length(3, 30);
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address is required.")
                             .EmailAddress().WithMessage("Your email address is not valid email address.");
        }
    }
}

