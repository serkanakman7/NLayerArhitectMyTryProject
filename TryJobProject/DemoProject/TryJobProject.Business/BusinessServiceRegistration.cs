using Core.BackgroundServices;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Security.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Reflection;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.BusinessRules.Auths;
using TryJobProject.Business.BusinessRules.Foods;
using TryJobProject.Business.Concrete;

namespace TryJobProject.Business
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IFoodService, FoodManager>();
            services.AddScoped<ICategoryService, CategoryManger>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ICustomerService, CustomerManager>();
            services.AddScoped<IAddressService, AddressManager>();
            services.AddHostedService<RabbitMQConsumer>();
            services.AddScoped<ITokenHelper, JwtHelper>();

            //Buraya bir tane rabbitmqSettings oluşturarak appsettings den gelen verileri ona at böylece kod okunaklılığı biraz daha artacaktır.RabbitMqSettings

            services.AddSingleton(sp => new ConnectionFactory()
            {
                HostName = configuration.GetSection("RabbitMQConfigurations")["Host"],
                Port = Convert.ToInt32(configuration.GetSection("RabbitMQConfigurations")["Port"]),
                UserName = configuration.GetSection("RabbitMQConfigurations")["UserName"],
                Password = configuration.GetSection("RabbitMQConfigurations")["Password"]
            });

            services.AddScoped<LoggerServiceBase, PostgreSqlLogger>();

            services.AddScoped<FoodBusinessRules>();
            services.AddScoped<AuthBusinessRules>();

            return services;
        }
    }
}
