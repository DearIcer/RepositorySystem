using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    /// <summary>
    /// 带有删除属性数据访问层接口
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    public interface IBaseDeleteDAL<T> : IBaseDAL<T> where T : BaseDeleteEntity
    {
        /// <summary>
        /// 返回实体类
        /// </summary>
        /// <param name="id">要查找的实体类的ID</param>
        /// <returns></returns>
        T GetEntityByID(string id);
    }
}
