using Core.CrossCuttingConcerns.Exception.Types;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ValidationException = Core.CrossCuttingConcerns.Exception.Types.ValidationException;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entities)
        {

            ValidationContext<object> context = new(entities);
            var result = validator.Validate(context);

            if (!result.IsValid)
            {

                ValidationExceptionModel? error = new()
                {
                    Property = result.Errors.Select(x => x.PropertyName).First(),
                    Errors = result.Errors.Select(x => x.ErrorMessage)
                };

                throw new ValidationException(error);
            }

        }
    }
}
