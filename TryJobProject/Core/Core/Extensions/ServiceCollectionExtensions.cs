﻿using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,params ICoreModule[] coreModules)
        {
            foreach (var coreModule in coreModules)
            {
                coreModule.Load(services);
            }
            return ServiceTool.Craete(services);
        }
    }
}
