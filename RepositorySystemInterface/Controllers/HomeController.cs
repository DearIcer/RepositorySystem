using Models;
using RepositorySystemInterface.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace RepositorySystemInterface.Controllers
{
    [CustomAttribute]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            //var UserName =  HttpContext.Session["UserName"];
            //var UserId = HttpContext.Session["UserId"];
            //if(UserName == null && UserId == null)
            //{          
            //    return RedirectToAction("LoginView", "Account");
            //}
 
            
            return View();
        }

        
    }
}