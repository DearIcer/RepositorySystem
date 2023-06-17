using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 带有软删除的基类
    /// </summary>
    public class BaseDeleteDAL<T> :BaseDAL<T>, IBaseDeleteDAL<T> where T : BaseDeleteEntity
    {
        //数据上下文
        private RepositorySystemContext _dbContext;
        public BaseDeleteDAL(RepositorySystemContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetEntityByID(string id)
        {
            return this._dbContext.Set<T>().FirstOrDefault(u => u.Id == id);
        }
    }
}
