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
    public class ConsumableInfoDAL : BaseDeleteDAL<ConsumableInfo>, IConsumableInfoDAL
    {
        private RepositorySystemContext _dbContext;
        public ConsumableInfoDAL(RepositorySystemContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<ConsumableInfo> GetConsumableInfo()
        {
            return _dbContext.ConsumableInfo;
        }
    }
}
