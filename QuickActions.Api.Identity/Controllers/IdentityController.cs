using Microsoft.AspNetCore.Mvc;
using QuickActions.Api.Identity.Services;
using QuickActions.Common.Data;
using QuickActions.Common.Interfaces;
using System.Net;
using System.Web.Http;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace QuickActions.Api.Identity.Controllers
{
    public abstract class IdentityController<T> : IIdentity<T>
    {
        protected readonly SessionsService<T> sessionsService;

        protected IdentityController(SessionsService<T> sessionsService)
        {
            this.sessionsService = sessionsService;
        }

        [HttpPost("logout")]
        public virtual async Task Logout()
        {
            sessionsService.DeleteSession();
        }

        [HttpPost("authenticate")]
        public virtual async Task<Session<T>> Authenticate()
        {
            return sessionsService.ReadSession() ?? throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}