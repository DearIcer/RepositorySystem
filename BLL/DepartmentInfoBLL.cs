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

            count = _departmentInfoDAL.GetDepartmentInfos().Count();
            var ListPage = tempList.OrderBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();
            return ListPage;
        }
    }
}
