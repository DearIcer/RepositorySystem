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

        /// <summary>
        /// 登录的接口
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string account,string password)
        {
            ReturnResult result = new ReturnResult();

            //判断账号密码合法性
            if(string.IsNullOrWhiteSpace(account))
            {
                result.Msg = "账号不能为空";
                return Json(result);
                //return new JsonHelper(result);
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                result.Msg = "密码不能为空";
                return Json(result);
                //return new JsonHelper(result);
            }

            string msg;
            string userName;
            string userId;

            // 调用登录业务
            bool isSuccess = _userInfoBLL.Login(account, password, out msg, out userName, out userId);

            result.Msg = msg;

            if(isSuccess)
            {
                result.IsSuccess = isSuccess;
                result.Status = 200;
                result.Datas = userName;
                //session

                HttpContext.Session["UserName"] = userName;
                HttpContext.Session["UserId"] = userId;

                return Json(result);
                //return new JsonHelper(result);
            }
            else
            {
                result.Status = 500;

                return Json(result);
                //return new JsonHelper(result);
            }
        }
    }
}