using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class UserInfoDAL
    {
        private RepositorySystemContext _DbContext;
        public UserInfoDAL()
        {
            _DbContext = new RepositorySystemContext();
        }
        public DbSet<UserInfo> GetUserInfos()
        {
            //RepositorySystemContext db = new RepositorySystemContext();

            return _DbContext.UserInfo;
        }
    }
}
