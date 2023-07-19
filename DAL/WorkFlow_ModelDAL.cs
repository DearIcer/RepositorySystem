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
    public class WorkFlow_ModelDAL : BaseDeleteDAL<WorkFlow_Model>, IWorkFlow_ModelDAL
    {
        private RepositorySystemContext _dbContext;
        public WorkFlow_ModelDAL(RepositorySystemContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public DbSet<WorkFlow_Model> GetWorkFlow_Model()
        {
            return _dbContext.WorkFlow_Model;
        }
    }
}
