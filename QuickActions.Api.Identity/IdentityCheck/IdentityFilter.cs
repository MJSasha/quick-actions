using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using QuickActions.Api.Identity.Services;
using System.Net;
using System.Reflection;
using System.Web.Http;

namespace QuickActions.Api.Identity.IdentityCheck
{
    public class IdentityFilter<T> : IActionFilter
    {
        private readonly SessionsService<T> sessionsService;

        public IdentityFilter(SessionsService<T> sessionsService)
        {
            this.sessionsService = sessionsService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller.GetType();
            var actionName = (context.ActionDescriptor as ControllerActionDescriptor)?.ActionName;

            MethodBase method = controller.GetMethod(actionName);
            var identity = (IdentityAttribute)method.GetCustomAttributes(typeof(IdentityAttribute), true).FirstOrDefault();

            if (identity == null) return;
            bool isIdentityMatched = sessionsService.CheckAccess(identity.RoleNames);

            if (!isIdentityMatched)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }
    }
}