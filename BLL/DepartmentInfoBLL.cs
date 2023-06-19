using IBLL;
using IDAL;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DepartmentInfoBLL : IDepartmentInfoBLL
    {
        /// <summary>
        /// 部门表
        /// </summary>
        private IDepartmentInfoDAL _departmentInfoDAL;
        /// <summary>
        /// 数据上下文
        /// </summary>
        private RepositorySystemContext _dbContext;
        public DepartmentInfoBLL(IDepartmentInfoDAL departmentInfoDAL, RepositorySystemContext dbContext)
        {
            _departmentInfoDAL = departmentInfoDAL;
            _dbContext = dbContext;
        }
        public List<GetDepartmentInfoDTO> GetDepartmentInfos(int page, int limit, string departmentInfoId, string departmentName, out int count)
        {
            #region Old

            ////部门表
            //var departmentList = _departmentInfoDAL.GetDepartmentInfos().ToList();
            ////分页
            //var listPage = departmentList.OrderByDescending(u => u.CreatedTime).Skip(limit * (page - 1)).Take(limit).ToList();

            //List<GetDepartmentInfoDTO> tempList = new List<GetDepartmentInfoDTO>();

            //count = departmentList.Count();

            //foreach (var item in listPage)
            //{
            //    GetDepartmentInfoDTO data = new GetDepartmentInfoDTO
            //    {
            //        DepartmentInfoId = item.Id,
            //        Description = item.Description,
            //        DepartmentName = item.DepartmentName,  
            //        LeaderId = item.LeaderId,
            //        ParentId = item.ParentId,
            //        CreateTime = item.CreatedTime
            //    };
            //    tempList.Add(data);
            //}

            //return tempList;

            #endregion
            //按照 CreatedTime 字段进行降序排列，然后使用 select 投影为一个新的实体对象类型 GetDepartmentInfoDTO 的集合，再使用 Skip 和 Take 方法实现分页功能，并最终返回查询结果及总记录数。
            var tempList = (from d in _departmentInfoDAL.GetDepartmentInfos()
                            orderby d.CreatedTime descending
                            select new GetDepartmentInfoDTO
                            {
                                DepartmentInfoId = d.Id,
                                Description = d.Description,
                                DepartmentName = d.DepartmentName,
                                LeaderId = d.LeaderId,
                                ParentId = d.ParentId,
                                CreateTime = d.CreatedTime
                            }).Skip(limit * (page - 1)).Take(limit).ToList();

            count = _departmentInfoDAL.GetDepartmentInfos().Count();
            var ListPage = tempList.OrderBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();
            return ListPage;
        }
    }
}
