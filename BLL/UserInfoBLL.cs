using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using DAL;
using IBLL;
using IDAL;
using Models;

namespace BLL
{
    /// <summary>
    /// 用户表的业务逻辑层
    /// </summary>

    public class UserInfoBLL : IUserInfoBLL
    {
        private IUserInfoDAL _userInfoDAL;

        public UserInfoBLL(IUserInfoDAL userInfoDAL)
        {
            //_userInfoDAL = new UserInfoDAL();
            _userInfoDAL = userInfoDAL;
        }
        /// <summary>
        /// 用户登录业务逻辑
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="msg">返回的消息</param>
        /// <param name="userName">返回的用户名</param>
        /// <param name="userID">返回的用户ID</param>
        /// <returns></returns>
        public bool Login(string account, string password, out string msg, out string userName, out string userID)
        {
            // 将密码加密
            string NewPassword = MD5Help.GenerateMD5(password);

            // 根据账号密码查询用户
            UserInfo userInfo = _userInfoDAL.GetUserInfos().FirstOrDefault(u => u.Account == account && u.PassWord == NewPassword);

            // 判断是否存在该用户
            userName = "";
            userID = "";
            if (userInfo == null)
            {
                msg = "用户名或者密码错误";
                return false;
            }
            else
            {
                msg = "成功";
                userName = userInfo.UserName;
                userID = userInfo.Id;
                return true;
            }
        }
    }
}
