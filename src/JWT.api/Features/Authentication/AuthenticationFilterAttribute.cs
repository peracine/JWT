using JWT.api.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JWT.api.Features.Authentication
{
    public class AuthenticationFilterAttribute : TypeFilterAttribute
    {
        public AuthenticationFilterAttribute() : base(typeof(SubscriptionFilter))
        {
        }

        private class SubscriptionFilter : IActionFilter
        {
            public void OnActionExecuting(ActionExecutingContext context)
            {
                var user = context.HttpContext.User.GetUser();
                if (user == null)
                {
                    context.Result = new ContentResult() { StatusCode = 401, Content = "Invalid or expired token." };
                    return;
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}
