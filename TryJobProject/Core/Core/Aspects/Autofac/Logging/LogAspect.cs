using Castle.Core.Configuration;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        private Type _loggerService;

        public LogAspect(Type loggerService)
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            if(loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new Exception("Böyle bir loglama türü bulunamadı");
            }
            _loggerService = loggerService;
        }

        protected override void OnBefore(IInvocation invocation)
        {

            var loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(_loggerService);
            List<LogParameter> LogParameter = new List<LogParameter>();

            for (int i = 0; i < invocation.Method.GetParameters().Length; i++)
            {
                LogParameter.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Type = invocation.Arguments[i].GetType().Name,
                    Value = invocation.Arguments[i]
                });
            }

            LogDetail logDetail = new LogDetail()
            {
                FullName = invocation.Method.DeclaringType.Namespace + "." + invocation.Method.Name,
                MethodName = invocation.Method.Name,
                User = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "?",
                LogParameters = LogParameter
            };

            loggerServiceBase.Info(JsonSerializer.Serialize(logDetail));
        }
    }
}
