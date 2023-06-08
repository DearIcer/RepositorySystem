using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositorySystemInterface.Controllers
{
    public class HomeController : Controller
    {
        private RepositorySystemContext db = new RepositorySystemContext();
        public ActionResult Index()
        {
            db.MenuInfo.ToList();
            return View();
        }

        
    }
}