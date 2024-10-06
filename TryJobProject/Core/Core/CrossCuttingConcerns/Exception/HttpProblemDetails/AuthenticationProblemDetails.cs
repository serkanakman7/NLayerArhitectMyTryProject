using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exception.HttpProblemDetails
{
    public class AuthenticationProblemDetails : ProblemDetails
    {
        public AuthenticationProblemDetails(string detail)
        {
            Title = "Authentication Violation";
            Detail = detail;
            Status = StatusCodes.Status400BadRequest;
            Type = "https://example.com/probs/authentication";
        }
    }
}
