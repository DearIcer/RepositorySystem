using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using CommonLib;
using IBLL;

namespace RepositorySystemInterface.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private IUserInfoBLL _userInfoBLL;
        public AccountController(IUserInfoBLL userInfoBLL)
        {
            //_userInfoBLL = new UserInfoBLL();
            _userInfoBLL = userInfoBLL;
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
        [HttpGet]
        public ActionResult Login(string account,string password)
        {
            ReturnResult result = new ReturnResult();

            //判断账号密码合法性
            if(string.IsNullOrWhiteSpace(account))
            {
                result.Msg = "账号不能为空";
                //return Json(result);
                return new JsonHelper(result);
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                result.Msg = "密码不能为空";
                //return Json(result);
                return new JsonHelper(result);
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
                result.Code = 200;
                result.Data = userName;
                //session

                HttpContext.Session["UserName"] = userName;
                HttpContext.Session["UserId"] = userId;

                // cookie
                HttpCookie cookie = new HttpCookie("UserID",userId);
                //cookie.Expires.AddHours(24);// 设置24小时后过期
                cookie.Expires = DateTime.Now.AddDays(100);
                Response.Cookies.Add(cookie);

                //return Json(result);
                return new JsonHelper(result);
            }
            else
            {
                result.Code = 500;

                //return Json(result);
                return new JsonHelper(result);
            }
        }
    }
}