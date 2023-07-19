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
    public class WorkFlow_InstanceDAL : BaseDAL<WorkFlow_Instance>, IWorkFlow_InstanceDAL
    {
        private RepositorySystemContext _dbContext;
        public WorkFlow_InstanceDAL(RepositorySystemContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<WorkFlow_Instance> GetWorkFlow_Instance()
        {
            return _dbContext.WorkFlow_Instance;
        }
    }
}
