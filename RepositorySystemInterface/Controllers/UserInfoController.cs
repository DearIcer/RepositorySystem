using CommonLib;
using IBLL;
using Models;
using Models.DTO;
using RepositorySystemInterface.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace RepositorySystemInterface.Controllers
{
    [CustomAttribute]
    public class UserInfoController : Controller
    {
        // GET: Account
        private IUserInfoBLL _userInfoBLL;
        public UserInfoController(IUserInfoBLL userInfoBLL)
        {
            //_userInfoBLL = new UserInfoBLL();
            _userInfoBLL = userInfoBLL;
        }
        // GET: UserInfo
        public ActionResult ListView()
        {
            return View();
        }

        public ActionResult CreateUserInfoView()
        {
            return View();

        }
        /// <summary>
        /// 获取用户的接口
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="account"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUserInfos(int page, int limit, string account, string userName)
        {
            int count;
            List<GetUserInfosDTO> list = _userInfoBLL.GetUserInfos(page, limit, account, userName, out count);

            ReturnResult result = new ReturnResult()
            {
                Code = 0,
                Msg = "获取成功",
                Data = list,
                IsSuccess = true,
                Count = count
            };

            return new JsonHelper(result);
        }
        /// <summary>
        /// 添加用户的接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUserInfo([Form] UserInfo user)
        {
            string msg;

            bool isSuccess = _userInfoBLL.CreateUserInfo(user, out msg);

            ReturnResult result = new ReturnResult();
            result.Msg = msg;
            result.IsSuccess = isSuccess;
            if (isSuccess)
            {
                result.Code = 200;
            }
            return new JsonHelper(result);
        }
    }
}