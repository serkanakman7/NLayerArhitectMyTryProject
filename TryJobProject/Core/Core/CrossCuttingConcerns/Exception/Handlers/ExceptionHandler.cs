using Core.CrossCuttingConcerns.Exception.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exception.Handlers
{
    public abstract class ExceptionHandler
    {
        public Task HandleExceptionAsync(System.Exception exception)
            => exception switch
            {
                BusinessException businessException => HandleException(businessException),
                ValidationException validationException => HandleException(validationException),
                AuthenticationException authenticationException => HandleException(authenticationException),
                _ => HandleException(exception)
            };

        protected abstract Task HandleException(BusinessException businessException);
        protected abstract Task HandleException(System.Exception exception);
        protected abstract Task HandleException(ValidationException validationException);
        protected abstract Task HandleException(AuthenticationException authenticationException);
    }
}
