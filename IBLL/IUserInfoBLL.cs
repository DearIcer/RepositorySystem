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
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CreateUserInfo(UserInfo entity, out string msg);
        /// <summary>
        /// 用户软删除
        /// </summary>
        /// <param name="id">要删除的用户ID</param>
        /// <returns></returns>
        bool DeleteUserInfo(string id);
        /// <summary>
        /// 批量软删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteUserInfo(List<string> ids);
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool UpdateUserInfo(UserInfo user , out string msg);
        /// <summary>
        /// 根据Id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GetUserInfosDTO GetUserInfoById(string id);
        /// <summary>
        /// 根据id修改用户密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="OldPassword"></param>
        /// <param name="NewPassword"></param>
        /// <param name="AgainPassword"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool UpdateUserInfoPassword(string Id,string OldPassword,string NewPassword,string AgainPassword,out string msg);
    }
}
