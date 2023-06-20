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
        private RepositorySystemContext _dbContext;
        /// <summary>
        /// 构造所需的基本数据
        /// </summary>
        /// <param name="userInfoDAL"></param>
        /// <param name="departmentInfoDAL"></param>
        /// <param name="dbContext"></param>
        public UserInfoBLL(IUserInfoDAL userInfoDAL, IDepartmentInfoDAL departmentInfoDAL, RepositorySystemContext dbContext)
        {
            _userInfoDAL = userInfoDAL;
            _departmentInfoDAL = departmentInfoDAL;
            _dbContext = dbContext;
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

            #region OLd
            //// 用户表
            //var userInfos = _userInfoDAL.GetUserInfos().Where(u => u.IsDelete == false);

            ////查找账号相同
            //if (!string.IsNullOrEmpty(account))
            //{
            //    userInfos = userInfos.Where(u => u.Account == account);
            //}
            ////查找姓名相同的
            //if (!string.IsNullOrEmpty(userName))
            //{
            //    userInfos = userInfos.Where(u => u.UserName.Contains(userName));
            //}

            //count = userInfos.Count();

            ////分页
            //var listPage = userInfos.OrderByDescending(u => u.CreatedTime).Skip(limit * (page - 1)).Take(limit).ToList();

            //List<GetUserInfosDTO> tempList = new List<GetUserInfosDTO>();

            ////部门表
            //var departmentList = _departmentInfoDAL.GetDepartmentInfos().ToList();

            //foreach (var item in listPage)
            //{
            //    var dt = departmentList.SingleOrDefault(d => d.Id == item.DepartmentId);
            //    GetUserInfosDTO data = new GetUserInfosDTO
            //    {
            //        UserId = item.Id,
            //        Account = item.Account,
            //        UserName = item.UserName,
            //        PhoneNum = item.PhoneNum,
            //        Email = item.Email,
            //        DepartmentName = dt == null ? "空" : dt.DepartmentName,
            //        Sex = item.Sex == 0 ? "女" : "男",
            //        CreateTime = item.CreatedTime
            //    };
            //    tempList.Add(data);
            //}
            

            //return tempList;
            #endregion

            #region New

            var data = from u in _dbContext.UserInfo.Where(u => u.IsDelete == false)
                       join d in _dbContext.DepartmentInfo.Where(d => d.IsDelete == false)
                       on u.DepartmentId equals d.Id
                       into tempUD
                       from dd in tempUD.DefaultIfEmpty()
                       select new GetUserInfosDTO
                       {
                           UserId = u.Id,
                           Account = u.Account,
                           UserName = u.UserName,
                           PhoneNum = u.PhoneNum,
                           Email = u.Email,
                           DepartmentName = dd.DepartmentName == null ? "空" : dd.DepartmentName,
                           Sex = u.Sex == 0 ? "女" : "男",
                           CreateTime = u.CreatedTime
                       };

            if (!string.IsNullOrWhiteSpace(userName))
            {
                data = data.Where(u => u.UserName.Contains(userName));
            }

            if (!string.IsNullOrWhiteSpace(account))
            {
                data = data.Where(u => u.Account == account);
            }

            count = data.Count();

            var ListPage = data.OrderBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();

            return ListPage;

            #endregion
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
        /// <summary>
        /// 添加用户的操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>反正值:真、假</returns>
        public bool CreateUserInfo(UserInfo entity , out string msg)
        {
            if (string.IsNullOrWhiteSpace(entity.Account))
            {
                msg = "用户名不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.PassWord))
            {
                msg = "密码不能为空";
                return false;
            }
            if(string.IsNullOrWhiteSpace(entity.UserName))
            {
                msg = "用户名不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.PhoneNum))
            {
                msg = "手机号不能为空";
                return false;
            }

            // 判断账号是否重复
            UserInfo user = _userInfoDAL.GetEntities().FirstOrDefault(u => u.Account == entity.Account);
            if (user != null)
            {
                msg = "用户账号已存在";
                return false;   
            }

            // 赋值id
            entity.Id = Guid.NewGuid().ToString();
            // Time
            entity.CreatedTime = DateTime.Now;
            // 更新到数据库
            bool isSuccess = _userInfoDAL.CreateEntity(entity);

            msg = isSuccess ? $"添加{entity.UserName}成功!" : "添加用户失败";
            
            return isSuccess;
        }
        /// <summary>
        /// 用户软删除
        /// </summary>
        /// <param name="id">要删除的用户ID</param>
        /// <returns></returns>
        public bool DeleteUserInfo(string id)
        {
            /*throw new NotImplementedException();*/
            
            // 根据Id查找用户是否存在
            UserInfo userInfo = _userInfoDAL.GetEntityByID(id);

            if (userInfo == null)
            {
                return false;
            }
            //修改用户状态
            userInfo.IsDelete = true;
            userInfo.DeleteTime = DateTime.Now;
            //返回结果
            return _userInfoDAL.UpdateEntity(userInfo);
        }
        /// <summary>
        /// 批量软删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteUserInfo(List<string> ids)
        {
            foreach (var item in ids)
            {
                // 根据用户ID查询用户
                UserInfo user = _userInfoDAL.GetEntityByID(item);
                if (user == null)
                {
                    continue;
                }
                user.IsDelete = true;
                user.DeleteTime = DateTime.Now;

                _userInfoDAL.UpdateEntity(user);
            }
            return true;
        }
    }
}
