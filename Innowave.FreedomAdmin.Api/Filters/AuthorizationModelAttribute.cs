using Innowave.FreedomAdmin.Api.Utilities;
using Innowave.FreedomAdmin.Api.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innowave.FreedomAdmin.Api.Filters
{
    public class AuthorizationModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string controllerName = context.RouteData.Values["controller"].ToString();

            if (controllerName == Constant.ProductController)
            {
                if (!(context.HttpContext.User.IsInRole(UserRoles.SUPER_ADMIN)))
                {
                    context.Result = new UnauthorizedResult();
                }
            }

            if (controllerName == Constant.BusinessController)
            {
                if (!(context.HttpContext.User.IsInRole(UserRoles.SUPER_ADMIN)))
                {
                    context.Result = new UnauthorizedResult();
                }
            }

            if (controllerName == Constant.AddressesController)
            {
                if (!(context.HttpContext.User.IsInRole(UserRoles.SUPER_ADMIN)))
                {
                    context.Result = new UnauthorizedResult();
                }
            }

        }
    }
}
