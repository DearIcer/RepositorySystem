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
using Models.DTO;

namespace BLL
{
    /// <summary>
    /// 用户表的业务逻辑层
    /// </summary>

    public class UserInfoBLL : IUserInfoBLL
    {
        private IUserInfoDAL _userInfoDAL;
        private IDepartmentInfoDAL _departmentInfoDAL;

        public UserInfoBLL(IUserInfoDAL userInfoDAL, IDepartmentInfoDAL departmentInfoDAL)
        {
            _userInfoDAL = userInfoDAL;
            _departmentInfoDAL = departmentInfoDAL;
        }

        /// <summary>
        /// 查询用户列表的函数
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="limit">每页几条数据</param>
        /// <param name="account">用户账号</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="count">数据总量</param>
        /// <returns></returns>
        public List<GetUserInfosDTO> GetUserInfos(int page, int limit, string account, string userName, out int count)
        {
            // 用户表
            var userInfos = _userInfoDAL.GetUserInfos().Where(u => u.IsDelete == false);

            //查找账号相同
            if (!string.IsNullOrEmpty(account))
            {
                userInfos = userInfos.Where(u => u.Account == account);
            }
            //查找姓名相同的
            if (!string.IsNullOrEmpty(userName))
            {
                userInfos = userInfos.Where(u => u.UserName.Contains(userName));
            }

            count = userInfos.Count();

            //分页
            var listPage = userInfos.OrderByDescending(u => u.CreatedTime).Skip(limit * (page - 1)).Take(limit).ToList();

            List<GetUserInfosDTO> tempList = new List<GetUserInfosDTO>();
            foreach (var item in listPage)
            {
                var dt = _departmentInfoDAL.GetDepartmentInfos().SingleOrDefault(d => d.Id == item.DepartmentId);
                GetUserInfosDTO data = new GetUserInfosDTO
                {
                    UserId = item.Id,
                    Account = item.Account,
                    UserName = item.UserName,
                    PhoneNum = item.PhoneNum,
                    Email = item.Email,
                    DepartmentName = dt == null ? "空" : dt.DepartmentName,
                    Sex = item.Sex == 0 ? "女" : "男",
                    CreateTime = item.CreatedTime
                };
                tempList.Add(data);
            }
            //部门表
            var departmentList = _departmentInfoDAL.GetDepartmentInfos().ToList();

            return tempList;
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
