using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IWorkFlow_InstanceBLL
    {
        /// <summary>
        /// 添加实例数据（申领记录）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CreateWorkFlow_Instance(WorkFlow_Instance entity, string userId,out string msg);

        /// <summary>
        /// 获取当前用户所有的申请记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<GetWorkFlow_InstanceDTO> GetWorkFlow_Instance(int page, int limit, string userId, out int count);
    }
}
