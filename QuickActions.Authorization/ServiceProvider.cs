using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using QuickActions.Api.Identity.Services;

namespace QuickActions.Api.Identity
{
    public static class ServiceProvider
    {
        /// <summary>
        /// <strong>IMPORTANT:</strong> add <see href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-7.0">IHttpContextAccessor</see> to DI
        /// </summary>
        public static IServiceCollection AddIdentity<T>(this IServiceCollection services, string keyName, long sessionLifeTime = long.MaxValue)
        {
            services.AddSingleton(sp => new SessionsService<T>(
                sp.GetRequiredService<IHttpContextAccessor>(),
                keyName,
                sessionLifeTime));

            return services;
        }
    }
}