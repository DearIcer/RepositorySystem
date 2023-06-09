using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using CommonLib;

namespace RepositorySystemInterface.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private UserInfoBLL _userInfoBLL;
        public AccountController()
        {
            _userInfoBLL = new UserInfoBLL();
        }
        public ActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string account,string password)
        {
            ReturnResult result = new ReturnResult();

            if(string.IsNullOrWhiteSpace(account))
            {
                result.Msg = "账号不能为空";
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                result.Msg = "密码不能为空";
                return Json(result);
            }
            string msg;
            string userName;
            string userId;

            bool isSuccess = _userInfoBLL.Login(account, password, out msg, out userName, out userId);

            result.Msg = msg;

            if(isSuccess)
            {
                result.IsSuccess = isSuccess;
                result.Status = 200;
                result.Datas = userName;
                //session
                //HttpContext.Session[UserName] = userName;

                return Json(result);
            }
            else
            {
                result.Status = 500;

                return Json(result);
            }
        }
    }
}