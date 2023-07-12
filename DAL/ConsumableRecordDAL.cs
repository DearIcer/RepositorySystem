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
    public class ConsumableRecordDAL : BaseDAL<ConsumableRecord>, IConsumableRecordDAL
    {
        private RepositorySystemContext _dbContext;
        public ConsumableRecordDAL(RepositorySystemContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<ConsumableRecord> GetConsumableRecord()
        {
            return _dbContext.ConsumableRecord;
        }
    }
}
