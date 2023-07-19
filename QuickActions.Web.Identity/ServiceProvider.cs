using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace QuickActions.Web.Identity
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddIdentity<T>(this IServiceCollection services, string keyName)
        {
            services.AddTransient(sp => new CookieService(sp.GetRequiredService<IJSRuntime>(), keyName));

            return services;
        }
    }
}