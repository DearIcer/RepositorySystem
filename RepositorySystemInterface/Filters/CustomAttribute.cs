using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositorySystemInterface.Filters
{
    public class CustomAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var UserCookie = filterContext.HttpContext.Request.Cookies["UserId"];
            if (UserCookie == null)
            {
                var result = new RedirectResult("/Account/LoginView");
                filterContext.Result = result;
            }

            base.OnActionExecuting(filterContext);
        }

    }


}