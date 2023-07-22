using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using QuickActions.Web.Identity.Services;

namespace QuickActions.Web.Identity
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddIdentity<T>(this IServiceCollection services, string keyName)
        {
            services
                .AddSingleton<SessionService<T>>();

            services.AddOptions();
            services.AddAuthorizationCore();

            services
                .AddScoped<AuthenticationStateProvider, TokenAuthStateProvider<T>>()
                .AddScoped(sp => new SessionCookieService(sp.GetRequiredService<IJSRuntime>(), keyName));

            return services;
        }
    }
}