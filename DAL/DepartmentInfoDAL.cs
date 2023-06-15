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
    public class DepartmentInfoDAL : IDepartmentInfoDAL
    {
        private RepositorySystemContext _dbContext;
        public DepartmentInfoDAL(RepositorySystemContext dbContext)
        {
            //_dbContext = new RepositorySystemContext();
            _dbContext = dbContext;
        }
        /// <summary>
        /// 获取用户表所有的数据
        /// </summary>
        /// <returns></returns>
        public DbSet<DepartmentInfo> GetDepartmentInfos()
        {
            //RepositorySystemContext db = new RepositorySystemContext();

            return _dbContext.DepartmentInfo;
        }

        //DbSet<Models.DepartmentInfo> IDepartmentInfoDAL.GetDepartmentInfo()
        //{
        //    //throw new NotImplementedException();
        //    return _dbContext.DepartmentInfo;
        //}
    }
}
