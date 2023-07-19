using Castle.DynamicProxy;
using QuickActions.Api.Identity.Services;
using System.Net;
using System.Reflection;
using System.Web.Http;

namespace QuickActions.Api.Identity.IdentityCheck
{
    public class IdentityInterceptor<T> : IInterceptor
    {
        private readonly SessionsService<T> sessionsService;

        public void Intercept(IInvocation invocation)
        {
            MethodInfo methodInfo = invocation.Method;

            if (Attribute.IsDefined(methodInfo, typeof(IdentityAttribute)))
            {
                IdentityAttribute identityAttribute = (IdentityAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(IdentityAttribute));

                bool isIdentityMatched = sessionsService.CheckAcess(identityAttribute.RoleNames);

                if (!isIdentityMatched)
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }
            }

            invocation.Proceed();
        }
    }
}