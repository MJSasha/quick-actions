using Microsoft.Extensions.DependencyInjection;

namespace QuickActions.Web.Identity
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddIdentity<T>(this IServiceCollection services, string keyName)
        {

            return services;
        }
    }
}