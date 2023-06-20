using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IDepartmentInfoBLL
    {
        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="limit">每页几条数据</param>
        /// <param name="departmentInfoId">部门ID</param>
        /// <param name="departmentName">部门名字</param>
        /// <param name="count">数据总量</param>
        /// <returns></returns>
        List<GetDepartmentInfoDTO> GetDepartmentInfos(int page, int limit, string departmentInfoId, string departmentName, out int count);
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <returns></returns>
        bool CreateDepartmentInfo(DepartmentInfo entity, out string msg);

    }
}
