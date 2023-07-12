using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class CategoryDAL : ICategoryDAL
    {
        private RepositorySystemContext _dbContext;
        public CategoryDAL(RepositorySystemContext dbContext) { _dbContext = dbContext; }
        public bool CreateEntity(Category entity)
        {
            _dbContext.Set<Category>().Add(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool DeleteEntity(Category entity)
        {
            if (entity == null)
            {
                return false;
            }
            else
            {
                _dbContext.Set<Category>().Remove(entity);
                return _dbContext.SaveChanges() > 0;
            }
        }

        public bool DeleteEntity(string Id)
        {
            // 先查找有没有这个实体
            Category entityToDelete = _dbContext.Set<Category>().FirstOrDefault(u => u.Id == Id);

            if (entityToDelete == null)
            {
                return false;
            }
            else
            {
                _dbContext.Set<Category>().Remove(entityToDelete);
                return _dbContext.SaveChanges() > 0;
            }
        }

        public DbSet<Category> GetCatgory()
        {
            return _dbContext.Set<Category>();
        }

        public bool UpdateEntity(Category entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 查询表的所有
        /// </summary>
        /// <returns></returns>
        public DbSet<Category> GetEntities()
        {
            return _dbContext.Set<Category>();
        }
    }
}
