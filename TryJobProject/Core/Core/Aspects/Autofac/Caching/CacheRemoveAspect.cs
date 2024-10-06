using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect:MethodInterception
    {
        private readonly IDistributedCache _cache;

        public CacheRemoveAspect()
        {
            _cache = ServiceTool.ServiceProvider.GetService<IDistributedCache>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            var cacheGroupKey = invocation.Method.ReflectedType?.FullName;

            byte[]? cachedGroup = _cache.Get(cacheGroupKey);

            if (cachedGroup!=null)
            {
                HashSet<string> keyInGroup = JsonSerializer.Deserialize<HashSet<string>>(Encoding.UTF8.GetString(cachedGroup));

                foreach(var key in keyInGroup)
                {
                    _cache.Remove(key);
                }
                _cache.Remove(cacheGroupKey);
                _cache.Remove($"{cacheGroupKey} SlidingExpiration");
            }
        }
    }
}
