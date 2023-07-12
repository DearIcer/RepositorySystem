using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface ICategoryDAL 
    {
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">添加的实体</param>
        /// <returns></returns>
        bool CreateEntity(Category entity);
        /// <summary>
        /// 删除数据，根据实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <returns></returns>
        bool DeleteEntity(Category entity);
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
        bool UpdateEntity(Category entity);
        DbSet<Category> GetCatgory();

        /// <summary>
        /// 查询整表的数据
        /// </summary>
        /// <returns></returns>
        DbSet<Category> GetEntities();

        
    }
}
