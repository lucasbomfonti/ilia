using Microsoft.AspNetCore.Mvc.Filters;

namespace Ilia.Api.Security
{
    public class AccessValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            UserManagement.Validate(context.HttpContext.Request);
            base.OnActionExecuting(context);
        }
    }
}