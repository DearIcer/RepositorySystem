using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using DAL;
using Models;

namespace BLL
{
    /// <summary>
    /// 用户表的业务逻辑层
    /// </summary>

    public class UserInfoBLL
    {
        private UserInfoDAL _userInfoDAL;

        public UserInfoBLL()
        {
            _userInfoDAL = new UserInfoDAL();
        }

        public bool Login(string account, string password, out string msg, out string userName, out string userID)
        {
            string NewPassword = MD5Help.GenerateMD5(password);

            UserInfo userInfo = _userInfoDAL.GetUserInfos().FirstOrDefault(u => u.Account == account && u.PassWord == password);


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
