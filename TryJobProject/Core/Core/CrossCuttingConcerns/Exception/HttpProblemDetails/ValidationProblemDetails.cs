using Core.CrossCuttingConcerns.Exception.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exception.HttpProblemDetails
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public ValidationExceptionModel Errors { get; init; }
        public ValidationProblemDetails(ValidationExceptionModel errors)
        {
            Title = "Validation error(s)";
            Detail = "One to more validation errors occurred";
            Errors = errors;
            Status = StatusCodes.Status400BadRequest;
            Type = "https://example.com/probs/validation";
        }
    }
}
