using Models;
using Models.DTO;
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

        /// <summary>
        /// 查询用户列表的函数
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="limit">每页几条数据</param>
        /// <param name="account">用户账号</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="count">数据总量</param>
        /// <returns></returns>
        List<GetUserInfosDTO> GetUserInfos(int page, int limit, string account, string userName, out int count);
    }
}
