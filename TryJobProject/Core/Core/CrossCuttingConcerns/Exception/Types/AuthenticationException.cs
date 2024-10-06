using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exception.Types
{
    public class AuthenticationException : System.Exception
    {
        public AuthenticationException() { }

        public AuthenticationException(string? message):base(message) { }

        public AuthenticationException(string? message,System.Exception? innerException):base(message,innerException) { }
    }
}
