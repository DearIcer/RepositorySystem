using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositorySystemInterface.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        public ActionResult ListView()
        {
            return View();
        }
    }
}