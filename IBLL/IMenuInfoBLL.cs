using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IMenuInfoBLL
    {
        /// <summary>
        /// 查询菜单列表的函数
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="limit">每页几条数据</param>
        /// <param name="account">角色ID</param>
        /// <param name="userName">角色名</param>
        /// <param name="count">返回的数据总量</param>
        /// <returns></returns>
        List<GetMenuInfoDTO> GetAllMenuInfos(int page, int limit, string id, string MenuName, out int count);

        /// <summary>
        /// 添加菜单信息
        /// </summary>
        /// <returns></returns>
        bool CreateMenuInfo(MenuInfo entity, out string msg);

        /// <summary>
        /// 菜单软删除
        /// </summary>
        /// <param name="id">要删除的菜单ID</param>
        /// <returns></returns>
        bool DeleteMenuInfo(string id);
        /// <summary>
        /// 菜单软删除部门
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteMenuInfos(List<string> ids);
        /// <summary>
        /// 返回菜单列表 非分页
        /// </summary>
        /// <returns></returns>
        List<GetMenuInfoDTO> GetAllMenuInfos();
    }
}
