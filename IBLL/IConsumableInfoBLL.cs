using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IConsumableInfoBLL
    {
        /// <summary>
        /// 查询菜单列表的函数
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="limit">每页几条数据</param>
        /// <param name="id">ID</param>
        /// <param name="ConsumableName">角色名</param>
        /// <param name="count">返回的数据总量</param>
        /// <returns></returns>
        List<GetConsumableInfoDTO> GetAllConsumableInfos(int page, int limit, string id, string ConsumableName, out int count);

        /// <summary>
        /// 添加耗材数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CreateConsumableInfo(ConsumableInfo entity, out string msg);

        /// <summary>
        /// 更新分类数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool UpdateConsumableInfo(ConsumableInfo entity, out string msg);

        /// <summary>
        /// 获取下拉列表数据
        /// </summary>
        /// <returns></returns>
        object GetSelectOptions();
    }
}
