using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CommonLib;
using Models;

namespace RepositorySystemInterface
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //只需要执行一次，用完记得注释！
            //InitDB();
        }

        public static void InitDB()
        {
            RepositorySystemContext db = new RepositorySystemContext();

            if (db.Database.Exists())
            {
                db.Database.Delete();
            }
            db.Database.Create();

            //
            UserInfo userInfo = new UserInfo()
            {
                Id = Guid.NewGuid().ToString(),
                Account = "admin",
                PassWord = MD5Help.GenerateMD5("123456"),
                CreatedTime = DateTime.Now,
                IsAdmin = true,
                UserName = "超级管理员"
            };

            db.UserInfo.Add(userInfo);
            db.SaveChanges();
        }

    }
}
