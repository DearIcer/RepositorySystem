using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 所有的数据访问层接口
    /// </summary>
    public class BaseDAL<T> : IBaseDAL<T> where T : BaseEntity
    {
        //数据上下文
        private RepositorySystemContext _dbContext;
        public BaseDAL(RepositorySystemContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CreateEntity(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 删除实体对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            else
            {
                _dbContext.Set<T>().Remove(entity);
                return _dbContext.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteEntity(string Id)
        {
            // 先查找有没有这个实体
            T entityToDelete = _dbContext.Set<T>().FirstOrDefault(u => u.Id == Id);

            if (entityToDelete == null)
            {
                return false;
            }
            else
            {
                _dbContext.Set<T>().Remove(entityToDelete);
                return _dbContext.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 查询表的所有
        /// </summary>
        /// <returns></returns>
        public DbSet<T> GetEntities()
        {
            return _dbContext.Set<T>();
        }
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateEntity(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return _dbContext.SaveChanges() > 0;
        }
    }
}
