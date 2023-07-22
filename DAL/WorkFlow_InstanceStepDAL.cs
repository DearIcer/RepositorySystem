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
    public class WorkFlow_InstanceStepDAL : BaseDAL<WorkFlow_InstanceStep>, IWorkFlow_InstanceStepDAL
    {
        private RepositorySystemContext _dbContext;

        public WorkFlow_InstanceStepDAL(RepositorySystemContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<WorkFlow_InstanceStep> GetWorkFlow_InstanceStep()
        {
            return _dbContext.WorkFlow_InstanceStep;
        }
    }
}
