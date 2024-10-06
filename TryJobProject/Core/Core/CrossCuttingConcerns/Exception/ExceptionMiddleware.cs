using Core.CrossCuttingConcerns.Exception.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpExceptionHandler _httpExceptionHandler;
        private readonly LoggerServiceBase _loggerServiceBase;

        public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, LoggerServiceBase loggerServiceBase)
        {
            _next = next;
            _httpExceptionHandler = new HttpExceptionHandler();
            _httpContextAccessor = httpContextAccessor;
            _loggerServiceBase = loggerServiceBase;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context.Response, ex);
                await LogExceptionAsync(context, ex);
            }
        }

        private Task LogExceptionAsync(HttpContext context, System.Exception ex)
        {
            List<LogParameter> parameters = new List<LogParameter>()
            {
                new LogParameter{Name=context.Request.ToString(),Type=context.GetType().Name,Value=ex.Message }
            };

            LogDetailWithException logDetailWithException = new LogDetailWithException()
            {
                ExceptionMessage = ex.Message,
                LogParameters = parameters,
                User = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "?",
                MethodName = _next.Method.Name,
                FullName = _next.GetMethodInfo().DeclaringType.Namespace + "." + _next.Method.Name,
            };

            _loggerServiceBase.Error(JsonSerializer.Serialize(logDetailWithException));

            return Task.CompletedTask;
        }

        private Task HandleExceptionAsync(HttpResponse response, System.Exception exception)
        {
            response.ContentType = "application/json";
            _httpExceptionHandler.Response = response;
            return _httpExceptionHandler.HandleExceptionAsync(exception);
        }
    }
}
