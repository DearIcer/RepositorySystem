using DAL;
using IBLL;
using IDAL;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RoleInfoBLL : IRoleInfoBLL
    {
        private RepositorySystemContext _dbContext;
        private IRoleInfoDAL _roleInfo;
        private IR_UserInfo_RoleInfoDAL _r_UserInfo_RoleInfoDAL;
        /// <summary>
        /// 接口数据实例化
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <param name="roleInfo"></param>
        public RoleInfoBLL(RepositorySystemContext dbcontext,IRoleInfoDAL roleInfo, IR_UserInfo_RoleInfoDAL r_UserInfo_RoleInfoDAL)
        {
            _dbContext = dbcontext;
            _roleInfo = roleInfo;
            _r_UserInfo_RoleInfoDAL = r_UserInfo_RoleInfoDAL;
        }
        /// <summary>
        /// 绑定用户角色
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool BindUserInfo(List<string> userIds, string roleId)
        {
            //throw new NotImplementedException();
            if(userIds == null || userIds.Count == 0) return false;

            List<R_UserInfo_RoleInfo> BindUserList = _r_UserInfo_RoleInfoDAL.GetEntities().Where(x => x.RoleId==roleId).ToList();

            foreach (var item in userIds)
            {
                //var user = BindUserList.FirstOrDefault(x => x.UserId == item);
                bool isHas = BindUserList.Any(x => x.UserId == item);
                if (!isHas)
                {
                    R_UserInfo_RoleInfo entity = new R_UserInfo_RoleInfo()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = item,
                        RoleId = roleId,
                        CreatedTime = DateTime.Now,
                    };
                    _r_UserInfo_RoleInfoDAL.CreateEntity(entity);
                }
            }
            return true;
        }

        /// <summary>
        /// 添加角色的接口
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CreateRoleInfo(RoleInfo entity, out string msg)
        {
            //throw new NotImplementedException();
            if (entity == null)
            {
                msg = "数据实体为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.RoleName))
            {
                msg = "角色名为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.Description))
            {
                msg = "描述为空";
                return false;
            }
            // 判断角色是否存在
            RoleInfo roleInfo = _roleInfo.GetEntities().FirstOrDefault(r => r.RoleName == entity.RoleName);
            if (roleInfo != null)
            {
                msg = "角色已存在";
                return false;
            }

            // 赋值id
            entity.Id = Guid.NewGuid().ToString();
            //赋值描述
            entity.Description = entity.Description;
            //创建时间
            entity.CreatedTime = DateTime.Now;
            //更新到数据库
            bool IsSuccess = _roleInfo.CreateEntity(entity);
            msg = IsSuccess ? $"添加{entity.RoleName}成功!" : "添加角色失败";
            return IsSuccess;
        }
        /// <summary>
        /// 删除角色的接口
        /// </summary>
        /// <param name="RoleInfoId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteRoleInfo(string RoleInfoId)
        {
            //throw new NotImplementedException();
            RoleInfo role = _roleInfo.GetEntityByID(RoleInfoId);
            if (role == null) { return false; }
            role.IsDelete =true;
            role.DeleteTime = DateTime.Now;

            return _roleInfo.UpdateEntity(role);
        }
        /// <summary>
        /// 批量角色删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteRoleInfo(List<string> ids)
        {
            //throw new NotImplementedException();
            foreach (var id in ids)
            {
                RoleInfo role = _roleInfo.GetEntityByID(id);
                if (role == null) { continue; }
                role.DeleteTime = DateTime.Now;
                role.IsDelete = true;
                _roleInfo.UpdateEntity(role);
            }


            return true;
        }

        /// <summary>
        /// 查询角色列表的函数
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="limit">每页几条数据</param>
        /// <param name="account">角色ID</param>
        /// <param name="userName">角色名</param>
        /// <param name="count">返回的数据总量</param>
        /// <returns></returns>
        public List<GetRoleInfoDTO> GetAllRoleInfos(int page, int limit, string id, string RoleName, out int count)
        {
            #region 测试显示数据

            //var roleInfo = _roleInfo.GetRoleInfos().Where(r => r.IsDelete == false);

            //count = roleInfo.Count();

            //var listPage = roleInfo.OrderByDescending(u => u.CreatedTime).Skip(limit * (page - 1)).Take(limit).ToList();

            //List<GetRoleInfoDTO> tempList = new List<GetRoleInfoDTO>();

            //foreach (var item in listPage)
            //{
            //    GetRoleInfoDTO data = new GetRoleInfoDTO()
            //    {
            //        RoleId = item.Id,
            //        RoleName = item.RoleName,
            //        Description = item.Description,
            //        CreateTime = item.CreatedTime
            //    };
            //    tempList.Add(data);
            //}

            //return tempList;
            #endregion

            # region 实际查询
            var tempList = (from r in _roleInfo.GetRoleInfos().Where(r => r.IsDelete == false)
                            //orderby r.CreatedTime descending
                            select new  GetRoleInfoDTO
                            {
                                Description = r.Description,
                                RoleName = r.RoleName,
                                RoleId = r.Id,
                                CreateTime = r.CreatedTime
                            }).ToList();
            count = _roleInfo.GetRoleInfos().Count();
            return tempList.OrderBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList(); ;
            #endregion
        }
        /// <summary>
        /// 获取角色已经绑定的用户id集
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<string> GetBindUserIds(string roleId)
        {
            //throw new NotImplementedException();
            // 查询当前角色已经绑定的用户id
            List<string> UserIds = _r_UserInfo_RoleInfoDAL.GetEntities()
                                                          .Where(x => x.RoleId == roleId)
                                                          .Select(x => x.UserId)
                                                          .ToList();
            return UserIds;
        }

        /// <summary>
        /// 添加角色的接口
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateRoleInfo(RoleInfo entity, out string msg)
        {
            if (entity == null)
            {
                msg = "数据实体为空";
                return false;
            }
            if (entity.Id == null)
            {
                msg = "数据访问异常，Id为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.RoleName))
            {
                msg = "角色名为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.Description))
            {
                msg = "描述为空";
                return false;
            }
            // 判断角色是否存在
            RoleInfo roleInfo = _roleInfo.GetEntities().FirstOrDefault(r => r.Id == entity.Id);
            if (roleInfo == null)
            {
                msg = "角色不存在";
                return false;
            }

            //赋值描述
            roleInfo.Description = entity.Description;
            //创建时间
            roleInfo.CreatedTime = DateTime.Now;

            //更新到数据库
            bool IsSuccess = _roleInfo.UpdateEntity(roleInfo);

            msg = IsSuccess ? $"修改{roleInfo.RoleName}成功!" : "修改角色失败";

            return IsSuccess;
        }
    }
}
