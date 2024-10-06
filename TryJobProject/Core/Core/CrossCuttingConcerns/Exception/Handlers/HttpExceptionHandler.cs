using Core.CrossCuttingConcerns.Exception.Extensions;
using Core.CrossCuttingConcerns.Exception.HttpProblemDetails;
using Core.CrossCuttingConcerns.Exception.Types;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exception.Handlers
{
    public class HttpExceptionHandler : ExceptionHandler
    {
        private HttpResponse _response;

        public HttpResponse Response
        {
            get => _response ?? throw new ArgumentNullException(nameof(_response));
            set => _response = value;
        }
        protected override Task HandleException(BusinessException businessException)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new BusinessProblemDetails(businessException.Message).AsJson();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(System.Exception exception)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            string details = new InternalServerErrorProblemDetails().AsJson();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(AuthenticationException authenticationException)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string detail = new AuthenticationProblemDetails(authenticationException.Message).AsJson();
            return Response.WriteAsync(detail);
        }

        protected override Task HandleException(ValidationException validationException)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string detail = new ValidationProblemDetails(validationException.Errors).AsJson();
            return Response.WriteAsync(detail);
        }
    }
}
