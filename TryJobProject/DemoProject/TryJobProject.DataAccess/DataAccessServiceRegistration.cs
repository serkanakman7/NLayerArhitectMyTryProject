using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.DataAccess.Concrete.Context;
using TryJobProject.DataAccess.Concrete.EntityFramework;
using TryJobProject.DataAccess.Abstract;

namespace TryJobProject.DataAccess
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<FoodWebsiteDbContext>(options 
                => options.UseNpgsql(configuration.GetConnectionString("FoodWebsiteConnectionString")));

            services.AddScoped<IAddressDal, AddressDal>();
            services.AddScoped<ICategoryDal, CategoryDal>();
            services.AddScoped<ICompanyDal, CompanyDal>();
            services.AddScoped<ICustomerDal, CustomerDal>();
            services.AddScoped<IFoodDal, FoodDal>();
            services.AddScoped<IOrderDal, OrderDal>();
            services.AddScoped<IUserDal, UserDal>();
            services.AddScoped<IOperationClaimDal, OperationClaimDal>();

            return services;
        }
    }
}
