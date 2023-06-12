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
        private RepositorySystemContext _dbContext;
        public UserInfoDAL()
        {
            _dbContext = new RepositorySystemContext();
        }
        /// <summary>
        /// 获取用户表所有的数据
        /// </summary>
        /// <returns></returns>
        public DbSet<UserInfo> GetUserInfos()
        {
            //RepositorySystemContext db = new RepositorySystemContext();

            return _dbContext.UserInfo;
        }
    }
}
