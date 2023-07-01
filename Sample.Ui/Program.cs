using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using Sample.Common.Interfaces;

namespace Sample.Ui
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services
                .AddRefitClient<IUsers>(new RefitSettings { })
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:8080/Users"));

            await builder.Build().RunAsync();
        }
    }
}