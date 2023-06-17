using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    /// <summary>
    ///  基础数据访问层接口
    /// </summary>
    public interface IBaseDAL<T> where T : BaseEntity
    {
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">添加的实体</param>
        /// <returns></returns>
        bool CreateEntity(T entity);
        /// <summary>
        /// 删除数据，根据实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <returns></returns>
        bool DeleteEntity(T entity);
        /// <summary>
        /// 删除数据，根据ID
        /// </summary>
        /// <param name="Id">要删除的数据ID</param>
        /// <returns></returns>
        bool DeleteEntity(string Id);
        /// <summary>
        /// 更新整个表的数据
        /// </summary>
        /// <param name="entity">传入实体类</param>
        /// <returns></returns>
        bool UpdateEntity(T entity);
        /// <summary>
        /// 查询整表的数据
        /// </summary>
        /// <returns></returns>
        DbSet<T> GetEntities();

    }
}
