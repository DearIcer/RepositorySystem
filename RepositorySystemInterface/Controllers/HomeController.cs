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
            return View();
        }

        
    }
}