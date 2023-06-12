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
            //base.OnActionExecuting(filterContext);
            var UserId = filterContext.HttpContext.Session["UserId"];
            if (UserId == null)
            {
                var result = filterContext.Result;

                filterContext.Result = new RedirectResult("/Account/LoginView");
            }
        }
    }
    

}