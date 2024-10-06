using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exception.Types
{
    public class ValidationException : System.Exception
    {
        public ValidationExceptionModel? Errors { get; set; }
        public ValidationException() : base()
        {
            Errors = null;
        }

        public ValidationException(string? message):base(message)
        {
            Errors = null;
        }

        public ValidationException(string? message,System.Exception? exception):base(message,exception)
        {
            Errors = null;
        }

        public ValidationException(ValidationExceptionModel errors):base(BuildErrorMessage(errors))
        {
            Errors = errors;
        }

        private static string? BuildErrorMessage(ValidationExceptionModel? errors)
        {
            string err = $"{Environment.NewLine} -- {errors.Property}:{string.Join(Environment.NewLine,values:errors.Errors ?? Array.Empty<string>())}";

            return $"Validation failed: {string.Join(string.Empty,err)}";
        }
    }

    public class ValidationExceptionModel
    {
        public string Property { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
