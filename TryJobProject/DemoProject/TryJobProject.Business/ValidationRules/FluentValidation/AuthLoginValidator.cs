using Core.Entites.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Business.ValidationRules.FluentValidation
{
    public class AuthLoginValidator : AbstractValidator<UserForLoginDto>
    {
        public AuthLoginValidator()
        {
            RuleFor(a=>a.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}
