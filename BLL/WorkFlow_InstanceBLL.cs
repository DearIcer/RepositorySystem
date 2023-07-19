using DAL;
using IBLL;
using IDAL;
using Models;
using Models.DTO;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WorkFlow_InstanceBLL : IWorkFlow_InstanceBLL
    {
        private RepositorySystemContext _dbContext;
        private IWorkFlow_InstanceDAL _workFlow_InstanceDAL;
        public WorkFlow_InstanceBLL(RepositorySystemContext dbContext, IWorkFlow_InstanceDAL workFlow_InstanceDAL)
        {
            _dbContext = dbContext;
            _workFlow_InstanceDAL = workFlow_InstanceDAL;
        }
        public bool CreateWorkFlow_Instance(WorkFlow_Instance entity,string userId, out string msg)
        {
            using (var tarnsaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //创建工作流实例
                    WorkFlow_Instance workFlow_Instance = new WorkFlow_Instance
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedTime = DateTime.Now,
                        Creator = userId,
                        Description = entity.Description,
                        ModelId = entity.ModelId,
                        OutNum = entity.OutNum,
                        Reason = entity.Reason,
                        OutGoodsId = entity.OutGoodsId,
                        Status = (int)WorkFlow_InstanceStatusEnum.审批中
                    };
                    _dbContext.WorkFlow_Instance.Add(workFlow_Instance);
                    bool b = _dbContext.SaveChanges() > 0;
                    if (b == false)
                    {
                        tarnsaction.Rollback();
                        msg = "发起申请记录创建失败！";
                        return false;
                    }

                    // 先查自己是什么部门，有没有部门
                    UserInfo userInfo = _dbContext.UserInfo.FirstOrDefault(u => u.Id == userId && u.IsDelete == false);
                    if (userInfo == null || string.IsNullOrWhiteSpace(userInfo.DepartmentId))
                    {
                        tarnsaction.Rollback();
                        msg = "当前申请用户没有部门";
                        return false;
                    }
                    // 查询部门领导
                    DepartmentInfo department = _dbContext.DepartmentInfo.FirstOrDefault(d => d.Id == userInfo.DepartmentId && d.IsDelete == false);
                    if (department == null || string.IsNullOrWhiteSpace(department.LeaderId))
                    {
                        tarnsaction.Rollback();
                        msg = "用户所属部门没有领导";
                        return false;
                    }
                    // 领导的角色是不是 ——部门经理
                    int count = (from ur in _dbContext.R_UserInfo_RoleInfo.Where(x => x.UserId == department.LeaderId)
                                 join r in _dbContext.RoleInfo.Where(x => x.RoleName == "部门经理")
                                 on ur.RoleId equals r.Id
                                 select r.RoleName).Count();
                    if (count <= 0)
                    {
                        tarnsaction.Rollback();
                        msg = "领导不是部门经理";
                        return false;
                    }
                    // 实例化工作步骤
                    WorkFlow_InstanceStep workFlow_InstanceStep = new WorkFlow_InstanceStep()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedTime = DateTime.Now,
                        InstanceId = workFlow_Instance.Id,
                        ReviewerId = department.LeaderId,
                        ReviewStatus = (int)WorkFlow_InstanceStepStatusEnum.审核中
                    };
                    _dbContext.WorkFlow_InstanceStep.Add(workFlow_InstanceStep);
                    bool b2 = _dbContext.SaveChanges() > 0;
                    if (b2 == false)
                    {
                        tarnsaction.Rollback();
                        msg = "工作流步骤发起失败";
                        return false;
                    }

                    msg = "申请成功";
                    tarnsaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tarnsaction.Rollback();
                    msg = ex.Message;
                    return false;
                    throw;
                }
            }
        }


        List<GetWorkFlow_InstanceDTO> IWorkFlow_InstanceBLL.GetWorkFlow_Instance(int page, int limit, string userId, out int count)
        {
            //var tempList = (from r in _workFlow_InstanceDAL.GetWorkFlow_Instance().Where(r => r.Id != null)
            //                select new GetWorkFlow_InstanceDTO
            //                {
            //                    Id = r.Id,
            //                    ModelId = r.ModelId,
            //                    Status = r.Status,
            //                    Description = r.Description,
            //                    Reason = r.Reason,
            //                    CreateTime = r.CreatedTime,
            //                    Creator = r.Creator,
            //                    OutGoodsId = r.OutGoodsId,
            //                    OutNum = r.OutNum,
            //                }).ToList();

            var tempList = from wi in _dbContext.WorkFlow_Instance.Where(wi => wi.Creator == userId)
                           join wm in _dbContext.WorkFlow_Model
                           on wi.ModelId equals wm.Id
                           into WiAndWm
                           from wiwm in WiAndWm.DefaultIfEmpty()

                           join c in _dbContext.ConsumableInfo
                           on wi.OutGoodsId equals c.Id
                           into WiAndC
                           from wic in WiAndC.DefaultIfEmpty()

                           join u in _dbContext.UserInfo
                           on wi.Creator equals u.Id
                           into WiAndU
                           from wiu in WiAndU.DefaultIfEmpty()
                           select new GetWorkFlow_InstanceDTO
                           {
                               Id = wi.Id,
                               ModelId = wi.ModelId,
                               Status = wi.Status,
                               Description = wi.Description,
                               Reason = wi.Reason,
                               CreateTime = wi.CreatedTime,
                               
                               OutGoodsId = wi.OutGoodsId,
                               OutNum = wi.OutNum,

                               Creator = wiu.UserName,
                               OutGoodsIdName = wic.ConsumableName,
                               ModelName = wiwm.Title
                           };
            count = _workFlow_InstanceDAL.GetWorkFlow_Instance().Count();
            return tempList.OrderBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();
        }
    }
}
