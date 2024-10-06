using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Exception.Types;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.Constants;

namespace TryJobProject.Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string[] _roles;

        public SecuredOperation(string roles)
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _roles = roles.Split(',');
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in roleClaims) 
            {
                if (_roles.Contains(role))
                {
                    return;
                }
            }
            throw new AuthenticationException(AuthenticationMessage.DontHaveAuthory);
        }
    }
}
