using DAL;
using IBLL;
using IDAL;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public bool CreateDepartmentInfo(DepartmentInfo entity, out string msg)
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                msg = "部门ID不能为空!";
            }

            if (string.IsNullOrWhiteSpace(entity.Description))
            {
                msg = "部门描述不能为空!";
            }

            if (string.IsNullOrWhiteSpace(entity.DepartmentName))
            {
                msg = "部门名字不能为空!";
            }

            if (string.IsNullOrWhiteSpace(entity.LeaderId))
            {
                msg = "主管ID不能为空!";
            }

            if (string.IsNullOrWhiteSpace(entity.ParentId))
            {
                msg = "父部门ID不能为空";
            }

            //entity.Id = Guid.NewGuid().ToString();
            // 判断ID 是否重复
            DepartmentInfo info = _departmentInfoDAL.GetEntities().FirstOrDefault(u => u.Id == entity.Id);
            if (info != null)
            {
                msg = "部门ID已存在";
                return false;
            }
            // 判断名称 是否重复
            DepartmentInfo info2 = _departmentInfoDAL.GetEntities().FirstOrDefault(u => u.DepartmentName == entity.DepartmentName);
            if (info2 != null)
            {
                msg = "部门名称已存在";
                return false;
            }

            entity.CreatedTime = DateTime.Now;

            bool isSuccess = _departmentInfoDAL.CreateEntity(entity);

            msg = isSuccess ? $"添加{entity.DepartmentName}成功!" : "添加用户失败";

            return isSuccess;

        }
        /// <summary>
        /// 部门软删除
        /// </summary>
        /// <param name="id">要删除的部门ID</param>
        /// <returns></returns>
        public bool DeleteDepartmentInfo(string id)
        {
            //throw new NotImplementedException();
            // 根据Id查找用户是否存在
            DepartmentInfo department = _departmentInfoDAL.GetEntityByID(id);

            if (department == null)
            {
                return false;
            }
            //修改用户状态
            department.IsDelete = true;
            department.DeleteTime = DateTime.Now;
            //返回结果
            return _departmentInfoDAL.UpdateEntity(department);
        }
        /// <summary>
        /// 批量软删除部门
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteDepartmentInfos(List<string> ids)
        {
            foreach (var item in ids)
            {
                // 根据用户ID查询部门
                DepartmentInfo department = _departmentInfoDAL.GetEntityByID(item);
                if (department == null)
                {
                    continue;
                }
                department.IsDelete = true;
                department.DeleteTime = DateTime.Now;

                _departmentInfoDAL.UpdateEntity(department);
            }
            return true;
        }
        /// <summary>
        /// 根据部门id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DepartmentInfo GetDepartmentInfoById(string id)
        {
            return _departmentInfoDAL.GetEntityByID(id);
        }

        /// <summary>
        /// 获取所有部门表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="departmentInfoId"></param>
        /// <param name="departmentName"></param>
        /// <param name="count"></param>
        /// <returns></returns>
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
            var tempList = (from d in _departmentInfoDAL.GetDepartmentInfos().Where(u => u.IsDelete == false)
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
          
            var data = from d in _dbContext.DepartmentInfo.Where(d => d.IsDelete == false)
                       join u in _dbContext.UserInfo.Where(u => u.IsDelete == false)
                       on d.LeaderId equals u.Id
                       into TempDU
                       from uu in TempDU.DefaultIfEmpty()
                       join d2 in _dbContext.DepartmentInfo.Where(d => d.IsDelete == false)
                       on d.ParentId equals d2.Id
                       into TempDD
                       from dd2 in TempDD.DefaultIfEmpty()
                       select new GetDepartmentInfoDTO
                       {
                           DepartmentInfoId = d.Id,
                           DepartmentName= d.DepartmentName,
                           LeaderId= d.LeaderId,
                           CreateTime = d.CreatedTime,
                           Description = d.Description,
                           ParentName = dd2.DepartmentName,
                           LeaderName = uu.UserName
                       };

            //count = _departmentInfoDAL.GetDepartmentInfos().Count();
            //var ListPage = tempList.OrderBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();
            count = data.Count();
            var ListPage = data.OrderBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();

            return ListPage;
        }
    /// <summary>
    /// 获取下来列表的返回数据
    /// </summary>
    /// <returns></returns>
    public object GetSelectOptions()
    {
        /*hrow new NotImplementedException();*/
        var parentSelect = _dbContext.DepartmentInfo.Where(d => !d.IsDelete)
                                                    .Select(d => new
                                                    {
                                                        value = d.Id,
                                                        title = d.DepartmentName
                                                    })
                                                     .ToList();
        var leaderSelect = _dbContext.UserInfo.Where(u => !u.IsDelete)
                                                .Select(u => new
                                                {
                                                    value = u.Id,
                                                    title = u.UserName
                                                })
                                                .ToList();

        var data = new
        {
            parentSelect,
            leaderSelect,
        };

        return data;
    }
    /// <summary>
    /// 更新部门数据
    /// </summary>
    /// <param name="department"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public bool UpdateDepartmentInfo(DepartmentInfo department, out string msg)
    {
        //throw new NotImplementedException();
        if (string.IsNullOrWhiteSpace(department.Id))
        {
            msg = "部门ID不能为空!";
        }

        if (string.IsNullOrWhiteSpace(department.Description))
        {
            msg = "部门描述不能为空!";
        }

        if (string.IsNullOrWhiteSpace(department.DepartmentName))
        {
            msg = "部门名字不能为空!";
        }

        if (string.IsNullOrWhiteSpace(department.LeaderId))
        {
            msg = "主管ID不能为空!";
        }

        if (string.IsNullOrWhiteSpace(department.ParentId))
        {
            msg = "父部门ID不能为空";
        }
        DepartmentInfo entity = _departmentInfoDAL.GetEntities().FirstOrDefault(u => u.Id == department.Id);
        if (entity == null)
        {
            msg = "部门账号不存在";
            return false;
        }
        entity.Id = department.Id;
        entity.DepartmentName = department.DepartmentName;
        entity.Description = department.Description;
        entity.LeaderId = department.LeaderId;
        entity.ParentId = department.ParentId;

        bool isOk = _departmentInfoDAL.UpdateEntity(entity);

        msg = isOk ? $"修改{entity.DepartmentName}成功!" : "添加修改失败";

        return isOk;
    }
}
}
