using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IUserInfoDAL : IBaseDeleteDAL<UserInfo>
    {
        // <summary>
        /// 获取所有的用户表
        /// </summary>
        /// <returns></returns>
        DbSet<UserInfo> GetUserInfos();
    }
}
