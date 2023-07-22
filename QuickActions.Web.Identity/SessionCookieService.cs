using Microsoft.JSInterop;

namespace QuickActions.Web.Identity
{
    public class SessionCookieService
    {
        private readonly IJSRuntime jSRuntime;
        private readonly string keyName;

        public SessionCookieService(IJSRuntime jSRuntime, string keyName)
        {
            this.jSRuntime = jSRuntime;
            this.keyName = keyName;
        }

        public async Task WriteCookieAsync(string value, int days = 365)
        {
            await jSRuntime.InvokeVoidAsync("blazorExtensions.WriteCookie", keyName, value, days);
        }
    }
}