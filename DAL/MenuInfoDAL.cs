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
    public class MenuInfoDAL : BaseDAL<MenuInfo>, IMenuInfoDAL
    {
        //数据上下文
        private RepositorySystemContext _dbContext;
        public MenuInfoDAL(RepositorySystemContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public MenuInfo GetEntityByID(string id)
        {
            return this._dbContext.Set<MenuInfo>().FirstOrDefault(u => u.Id == id);
        }

        public DbSet<MenuInfo> GetMenuInfos()
        {
            return _dbContext.MenuInfo;
        }
    }
}
