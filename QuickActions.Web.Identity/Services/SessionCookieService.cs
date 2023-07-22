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

        public async Task WriteSessionKey(string value, int days = 365)
        {
            await jSRuntime.InvokeVoidAsync("WriteCookie.WriteCookie", keyName, value, days);
        }

        public async Task<string> ReadSessionKey()
        {
            return await jSRuntime.InvokeAsync<string>("ReadCookie.ReadCookie", keyName);
        }
    }
}