using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IUserInfoBLL
    {
        /// <summary>
        /// 用户登录业务逻辑
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="msg">返回的消息</param>
        /// <param name="userName">返回的用户名</param>
        /// <param name="userID">返回的用户ID</param>
        /// <returns></returns>
        bool Login(string account, string password, out string msg, out string userName, out string userID);

    }
}
