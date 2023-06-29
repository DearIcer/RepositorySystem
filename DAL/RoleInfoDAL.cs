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
    public class RoleInfoDAL : BaseDeleteDAL<RoleInfo>, IRoleInfoDAL
    {
        private RepositorySystemContext _dbContext;
        public RoleInfoDAL(RepositorySystemContext dbContext) : base(dbContext)
        {         
            this._dbContext = dbContext;
        }

        public DbSet<RoleInfo> GetRoleInfos()
        {
            return _dbContext.RoleInfo;
        }
    }
}
