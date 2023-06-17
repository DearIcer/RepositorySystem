using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IDepartmentInfoDAL : IBaseDeleteDAL<DepartmentInfo>
    {
        /// <summary>
        /// 获取所有的部门表
        /// </summary>
        /// <returns></returns>
        DbSet<DepartmentInfo> GetDepartmentInfos();
    }
}
