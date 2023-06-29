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
            //testdate();
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
        public static void testdate()
        {
            RepositorySystemContext db = new RepositorySystemContext();
            // 测试数据源------------------------------------------------


            //DepartmentInfo departmentInfo = new DepartmentInfo()
            //{
            //    Id = "000",
            //    Description = "公司高管",
            //    DepartmentName = "董事会",
            //    LeaderId = "001",
            //    ParentId = "000",
            //    CreatedTime = DateTime.Now,
            //};

            RoleInfo roleInfo = new RoleInfo()
            { 
                Id = "001",
                CreatedTime = DateTime.Now,
                RoleName = "Test",
                Description = "Test",
            };
            db.RoleInfo.Add(roleInfo);
            db.SaveChanges();
        }
    }
}
