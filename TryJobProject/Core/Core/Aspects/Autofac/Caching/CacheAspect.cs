using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Text;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.Json;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private readonly IDistributedCache _cache;
        private readonly int _slidingExpiration;

        public CacheAspect(int slidingExpiration = 2)
        {
            _cache = ServiceTool.ServiceProvider.GetService<IDistributedCache>();
            _slidingExpiration = slidingExpiration;
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType?.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments;

            var cacheGroupKey = invocation.Method.ReflectedType?.FullName;

            var cacheKey = $"{methodName}({string.Join(',', BuildKey(arguments))})";

            Type? returnType = invocation.Method.ReturnType.GenericTypeArguments.FirstOrDefault();

            byte[]? cachedResponse = _cache.Get(cacheKey);

            if (cachedResponse != null)
            {
                var result = ServiceStack.Text.JsonSerializer.DeserializeFromString(Encoding.Default.GetString(cachedResponse), returnType);

                invocation.ReturnValue = typeof(Task) ?
                    .GetMethod(nameof(Task.FromResult))
                    .MakeGenericMethod(returnType)
                    .Invoke(this, new object[] { result });

                return;
            }
            else
            {
                invocation.Proceed();
                TimeSpan slidingExpiration = TimeSpan.FromDays(_slidingExpiration);

                DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = slidingExpiration };

                byte[] serilazedData = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(invocation.ReturnValue.GetType().GetProperty("Result").GetValue(invocation.ReturnValue)));

                _cache.SetAsync(cacheKey, serilazedData, cacheOptions);

                if (cacheGroupKey != null)
                {
                    addCacheKeyToGroup(cacheKey, cacheGroupKey, slidingExpiration);
                }
            }
        }

        private void addCacheKeyToGroup(string cacheKey, string cacheGroupKey, TimeSpan slidingExpiration)
        {
            byte[]? cacheGroupCache = _cache.Get(key: cacheGroupKey!);
            HashSet<string> cacheKeysInGroup;

            if (cacheGroupCache != null)
            {
                cacheKeysInGroup = System.Text.Json.JsonSerializer.Deserialize<HashSet<string>>(Encoding.Default.GetString(cacheGroupCache));

                if (!cacheKeysInGroup.Contains(cacheKey))
                    cacheKeysInGroup.Add(cacheKey);
            }
            else
            {
                cacheKeysInGroup = new HashSet<string>(new[] { cacheKey });
            }

            byte[] newCacheGroupCache = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(cacheKeysInGroup);

            byte[]? cacheGroupCacheSlidingExpirationCache = _cache.Get(key: $"{cacheGroupKey} SlidingExpiration");

            int? cacheGroupCacheSlidingExpirationValue = null;

            if (cacheGroupCacheSlidingExpirationValue != null)
                cacheGroupCacheSlidingExpirationValue = Convert.ToInt32(Encoding.Default.GetString(cacheGroupCacheSlidingExpirationCache));

            if (cacheGroupCacheSlidingExpirationValue == null || slidingExpiration.TotalSeconds > cacheGroupCacheSlidingExpirationValue)
                cacheGroupCacheSlidingExpirationValue = Convert.ToInt32(slidingExpiration.TotalSeconds);

            byte[] serializeCachedGroupSlidingExpirationData = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(cacheGroupCacheSlidingExpirationValue);

            DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = TimeSpan.FromSeconds(Convert.ToDouble(cacheGroupCacheSlidingExpirationValue)) };
            _cache.Set(key: cacheGroupKey!, newCacheGroupCache, cacheOptions);

            _cache.Set(key: $"{cacheGroupKey} SlidingExpiration", serializeCachedGroupSlidingExpirationData, cacheOptions);
        }

        private string BuildKey(object[] args)
        {
            var sb = new StringBuilder();
            foreach (var arg in args)
            {
                var paramValues = arg.GetType().GetProperties()
                    .Select(p => p.GetValue(arg)?.ToString() ?? string.Empty);
                sb.Append(string.Join('_', paramValues));
            }

            return sb.ToString();
        }
    }
}

