using Autofac.Core;
using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Logging.Serilog.Messages;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class PostgreSqlLogger : LoggerServiceBase
    {
        protected IConfiguration Configuration { get; }


        public PostgreSqlLogger()
        {
            Configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>(); 

            var logConfiguration = Configuration.GetSection("SeriLogConfigurations:PostgreSqlConfiguration").Get<PostgreSqlConfiguration>()
                ?? throw new System.Exception(SerilogMessages.NullOptionsMessage);

            global::Serilog.Core.Logger serilogConfig = new LoggerConfiguration().WriteTo.PostgreSQL(logConfiguration.ConnectionString, logConfiguration.TableName, ColumnOptions.Default, needAutoCreateTable: logConfiguration.AutoCreateSqlTable)
                .CreateLogger();

            Logger = serilogConfig;
        }
    }
}
