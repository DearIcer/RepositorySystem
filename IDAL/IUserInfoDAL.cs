using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IUserInfoDAL
    {
        DbSet<UserInfo> GetUserInfos();
    }
}
