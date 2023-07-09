using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IRoleInfoBLL
    {
        /// <summary>
        /// 查询角色列表的函数
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="limit">每页几条数据</param>
        /// <param name="account">角色ID</param>
        /// <param name="userName">角色名</param>
        /// <param name="count">返回的数据总量</param>
        /// <returns></returns>
        List<GetRoleInfoDTO> GetAllRoleInfos(int page, int limit, string id, string RoleName, out int count);
        /// <summary>
        /// 添加角色的接口
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CreateRoleInfo(RoleInfo entity, out string msg);
        /// <summary>
        /// 修改角色的接口
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool UpdateRoleInfo(RoleInfo entity, out string msg);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="RoleInfoId"></param>
        /// <returns></returns>
        bool DeleteRoleInfo(string RoleInfoId);
        bool DeleteRoleInfo(List<string> ids);
        /// <summary>
        /// 获取角色已经绑定的用户id集
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<string> GetBindUserIds(string roleId);
        /// <summary>
        /// 绑定用户角色
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool BindUserInfo(List<string> userIds ,string roleId);
        /// <summary>
        /// 绑定菜单
        /// </summary>
        /// <param name="menuIds"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool BindMenuInfo(List<string> menuIds ,string roleId);
        /// <summary>
        /// 获取绑定菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<string>GetBindMenuIds(string roleId);
    }
}
