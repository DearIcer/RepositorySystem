using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Models;

namespace DAL
{
    public class UserInfoDAL : BaseDeleteDAL<UserInfo>,IUserInfoDAL
    {
        private RepositorySystemContext _dbContext;
        public UserInfoDAL(RepositorySystemContext dbContext) : base(dbContext)
        {
            //_dbContext = new RepositorySystemContext();
            this._dbContext = dbContext;
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
