using DAL;
using IBLL;
using IDAL;
using Models;
using Models.DTO;
using Models.Enums;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WorkFlow_InstanceStepBLL : IWorkFlow_InstanceStepBLL
    {
        private RepositorySystemContext _dbContext;
        private IWorkFlow_InstanceStepDAL _InstanceStepDAL;
        public WorkFlow_InstanceStepBLL(RepositorySystemContext repositorySystemContext, IWorkFlow_InstanceStepDAL InstanceStepDAL ) 
        {
            _dbContext = repositorySystemContext;
            _InstanceStepDAL = InstanceStepDAL;
        }
        public bool CreateWorkFlow_InstanceStep(WorkFlow_InstanceStepDTO entity, string userId, out string msg)
        {
            throw new NotImplementedException();
        }

        public List<WorkFlow_InstanceStepDTO> GetWorkFlow_InstanceStep(int page, int limit, string userId, out int count)
        {
            var tempList = from ws in _dbContext.WorkFlow_InstanceStep.Where (x => x.ReviewerId == userId)
                           join wi in _dbContext.WorkFlow_Instance
                           on ws.InstanceId equals wi.Id
                           into WsAndWi
                           from wswi in WsAndWi.DefaultIfEmpty()

                           join c in _dbContext.ConsumableInfo
                           on wswi.OutGoodsId equals c.Id
                           into WsAndC
                           from wsc in WsAndC.DefaultIfEmpty()

                           join u in _dbContext.UserInfo
                           on wswi.Creator equals u.Id
                           into WsAndU
                           from wsu in WsAndU.DefaultIfEmpty()

                           join wm in _dbContext.WorkFlow_Model
                           on wswi.ModelId equals wm.Id
                           into WsAnWm
                           from wswm in WsAnWm.DefaultIfEmpty()

                           join u2 in _dbContext.UserInfo
                           on ws.ReviewerId equals u2.Id
                           into WsAndU2
                           from wsu2 in WsAndU2.DefaultIfEmpty()

                           select new WorkFlow_InstanceStepDTO
                           {
                                Id = ws.Id,
                                ModelTitle = wswm.Title,
                                InstanceId = wswi.Id,
                                Reason = wswi.Reason,
                                CreatorName = wsu.UserName,
                                CreatorId = wsu.Id,

                                ReviewerId = ws.ReviewerId,
                                ReviewerName = wsu2.UserName,
                                ReviewReason = ws.ReviewReason,
                                ReviewStatus = ws.ReviewStatus,
                                ReviewTime = ws.ReviewTime,

                                CreateTime = ws.CreatedTime,
                                OutGoodsName = wsc.ConsumableName,
                                OutNum = wswi.OutNum,

                           };
            count = _InstanceStepDAL.GetWorkFlow_InstanceStep().Count();
            return tempList.OrderBy(u => u.ReviewStatus).ThenBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();
        }

        public bool UpdateWorkFlow_InstanceStep(string id, int outNum, string reviewReason, string userId, WorkFlow_InstanceStepStatusEnum workFlow_InstanceStepStatusEnum, out string msg)
        {
           using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    #region 先处理自己审核自己的操作
                    WorkFlow_InstanceStep workFlow_InstanceStep = _dbContext.WorkFlow_InstanceStep.FirstOrDefault(u => u.Id == id);
                    if (workFlow_InstanceStep == null)
                    {
                        msg = "找不到要审核的工作流步骤";
                        transaction.Rollback();
                        return false;
                    }
                    if(workFlow_InstanceStep.ReviewerId != userId)
                    {
                        msg = "你没有权限审核";
                        transaction.Rollback();
                        return false;
                    }
                    workFlow_InstanceStep.ReviewReason = reviewReason;
                    workFlow_InstanceStep.ReviewStatus = (int) workFlow_InstanceStepStatusEnum;
                    workFlow_InstanceStep.ReviewTime = DateTime.Now;

                    //提交修改
                    _dbContext.Entry(workFlow_InstanceStep).State = System.Data.Entity.EntityState.Modified;
                    bool isOk = _dbContext.SaveChanges() > 0;
                    if (isOk == false)
                    {
                        msg = "审核失败";
                        transaction.Rollback();
                        return false;
                    }

                    #endregion
                    // 先查询当前登录的账号是什么角色（普通职员、部门领导、仓管），不同角色走不同的结果
                    List<string> roleName = (from ur in _dbContext.R_UserInfo_RoleInfo.Where(x => x.UserId == userId)
                                             join r in _dbContext.RoleInfo
                                             on ur.RoleId equals r.Id
                                             select r.RoleName).ToList();

                    if(roleName.Contains("部门经理"))
                    {
                        if (workFlow_InstanceStepStatusEnum == WorkFlow_InstanceStepStatusEnum.同意)
                        {
                            // 第一步，查询所有角色为仓库管理员的账号id集
                            List<string> ckUserIds = (from r in _dbContext.RoleInfo.Where(x => x.RoleName == "仓库管理员")
                                                      join ur in _dbContext.R_UserInfo_RoleInfo
                                                      on r.Id equals ur.RoleId
                                                      select ur.UserId).ToList();
                            //先处理一个仓库管理员的情况
                            string ckUserId = ckUserIds.FirstOrDefault();
                            if(string.IsNullOrWhiteSpace(ckUserId))
                            {
                                msg = "找不到仓库管理员";
                                transaction.Rollback();
                                return false;
                            }
                            //第二步：给这个 仓库管理员创建一个工作流步骤
                            WorkFlow_InstanceStep newWork = new WorkFlow_InstanceStep()
                            {
                                Id = Guid.NewGuid().ToString(),
                                BeforeStepId = id,
                                InstanceId = workFlow_InstanceStep.InstanceId,
                                CreatedTime = DateTime.Now,
                                ReviewerId = ckUserId,
                                ReviewStatus = (int)WorkFlow_InstanceStatusEnum.审批中
                            };
                            _dbContext.WorkFlow_InstanceStep.Add(newWork);
                            isOk = _dbContext.SaveChanges() > 0;
                            if(isOk == false)
                            {
                                msg = "给仓库管理员创建工作流步骤时失败";
                                transaction.Rollback();
                                return false;
                            }
                        }
                        else if (workFlow_InstanceStepStatusEnum == WorkFlow_InstanceStepStatusEnum.驳回)
                        {
                            //第一步：先查询普通员工id，根据工作流实例记录去查找
                            WorkFlow_Instance workFlow_Instance = _dbContext.WorkFlow_Instance.FirstOrDefault(x => x.Id == workFlow_InstanceStep.InstanceId);
                            if(workFlow_Instance == null)
                            {
                                msg = "查询不到工作流实例";
                                transaction.Rollback();
                                return false;
                            }
                            //第二步：给这个 普通员工创建一个工作流步骤
                            WorkFlow_InstanceStep newWork = new WorkFlow_InstanceStep()
                            {
                                Id = Guid.NewGuid().ToString(),
                                BeforeStepId = id,
                                InstanceId = workFlow_InstanceStep.InstanceId,
                                CreatedTime = DateTime.Now,
                                ReviewerId = workFlow_Instance.Creator,
                                ReviewStatus = (int)WorkFlow_InstanceStatusEnum.审批中
                            };
                            _dbContext.WorkFlow_InstanceStep.Add(newWork);
                            isOk = _dbContext.SaveChanges() > 0;
                            if (isOk == false)
                            {
                                msg = "驳回失败";
                                transaction.Rollback();
                                return false;
                            }
                        }
                        else
                        {
                            msg = "你的操作错误";
                            transaction.Rollback();
                            return false;
                        }
                    }
                    else if(roleName.Contains("仓库管理员"))
                    {
                        if (workFlow_InstanceStepStatusEnum == WorkFlow_InstanceStepStatusEnum.同意)
                        {
                            // 第一步：先结束工作流实例
                            WorkFlow_Instance workFlow_Instance = _dbContext.WorkFlow_Instance.FirstOrDefault(x => x.Id == workFlow_InstanceStep.InstanceId);
                            if (workFlow_Instance == null)
                            {
                                msg = "找不到工作流实例";
                                transaction.Rollback();
                                return false;
                            }
                            workFlow_Instance.Status = (int)WorkFlow_InstanceStatusEnum.结束;
                            _dbContext.Entry(workFlow_Instance).State = System.Data.Entity.EntityState.Modified;
                            isOk = _dbContext.SaveChanges() > 0;
                            if (isOk == false)
                            {
                                msg = "结束工作流实例失败";
                                transaction.Rollback();
                                return false;
                            }
                            //第二步：减少耗材库存
                            ConsumableInfo consumable = _dbContext.ConsumableInfo.FirstOrDefault(x => x.Id == workFlow_Instance.OutGoodsId);
                            if (consumable == null)
                            {
                                msg = "耗材不存在";
                                transaction.Rollback();
                                return false;
                            }
                            if(consumable.Num - workFlow_Instance.OutNum < 0)
                            {
                                msg = "耗材库存不足";
                                transaction.Rollback();
                                return false;
                            }
                            consumable.Num -= workFlow_Instance.OutNum;
                            _dbContext.Entry(consumable).State = System.Data.Entity.EntityState.Modified;
                            isOk = _dbContext.SaveChanges() > 0;
                            if (isOk == false)
                            {
                                msg = "库存错误";
                                transaction.Rollback();
                                return false;
                            }
                            //增加耗材日志记录
                            ConsumableRecord consumableRecord = new ConsumableRecord()
                            {
                                Id = Guid.NewGuid().ToString(),
                                ConsumableId = consumable.Id,
                                CreatedTime = DateTime.Now,
                                Creator = userId,
                                Num = workFlow_Instance.OutNum,
                                Type = (int)ConsumableRecordTypeEnums.出库
                            };
                            _dbContext.ConsumableRecord.Add(consumableRecord);
                            isOk = _dbContext.SaveChanges() > 0;
                            if (isOk == false)
                            {
                                msg = "创建出库记录失败";
                                transaction.Rollback();
                                return false;
                            }
                        }
                        else if (workFlow_InstanceStepStatusEnum == WorkFlow_InstanceStepStatusEnum.驳回)
                        {
                            //第一步： 找到上一个步骤的审核人是谁
                            WorkFlow_InstanceStep oldWork = _dbContext.WorkFlow_InstanceStep.FirstOrDefault(x => x.Id == workFlow_InstanceStep.BeforeStepId);
                            if (oldWork == null)
                            {
                                msg = "无法找到上一个步骤";
                                transaction.Rollback();
                                return false;
                            }
                            //第二步：给上一级的人创建一条新的工作流步骤，让他重新审核
                            WorkFlow_InstanceStep newWork = new WorkFlow_InstanceStep()
                            {
                                Id = Guid.NewGuid().ToString(),
                                BeforeStepId = id,
                                InstanceId = workFlow_InstanceStep.InstanceId,
                                CreatedTime = DateTime.Now,
                                ReviewerId = oldWork.ReviewerId,
                                ReviewStatus = (int)WorkFlow_InstanceStatusEnum.审批中
                            };
                            _dbContext.WorkFlow_InstanceStep.Add(newWork);
                            isOk = _dbContext.SaveChanges() > 0;
                            if (isOk == false)
                            {
                                msg = "驳回失败";
                                transaction.Rollback();
                                return false;
                            }
                        }
                        else
                        {
                            msg = "你的操作错误";
                            transaction.Rollback();
                            return false;
                        }
                    }
                    else if(roleName.Contains("普通职员"))
                    {
                        //第一步：
                        WorkFlow_Instance workFlow_Instance = _dbContext.WorkFlow_Instance.FirstOrDefault(x => x.Id == workFlow_InstanceStep.InstanceId);
                        if (workFlow_Instance == null)
                        {
                            msg = "找不到工作流实例";
                            transaction.Rollback();
                            return false;
                        }
                        workFlow_Instance.OutNum = outNum;
                        _dbContext.Entry(workFlow_Instance).State = System.Data.Entity.EntityState.Modified;
                        isOk = _dbContext.SaveChanges() > 0;
                        if (isOk == false)
                        {
                            msg = "修改工作流实例申请耗材数量失败";
                            transaction.Rollback();
                            return false;
                        }
                        //第二步：
                        WorkFlow_InstanceStep oldWork = _dbContext.WorkFlow_InstanceStep.FirstOrDefault(x => x.Id == workFlow_InstanceStep.BeforeStepId);
                        if (oldWork == null)
                        {
                            msg = "无法找到上一个步骤";
                            transaction.Rollback();
                            return false;
                        }
                        //第三步：通知部门经理重新审核
                        WorkFlow_InstanceStep newWork = new WorkFlow_InstanceStep()
                        {
                            Id = Guid.NewGuid().ToString(),
                            BeforeStepId = id,
                            InstanceId = workFlow_InstanceStep.InstanceId,
                            CreatedTime = DateTime.Now,
                            ReviewerId =oldWork.ReviewerId,
                            ReviewStatus = (int)WorkFlow_InstanceStatusEnum.审批中
                        };
                        _dbContext.WorkFlow_InstanceStep.Add(newWork);
                        isOk = _dbContext.SaveChanges() > 0;
                        if (isOk == false)
                        {
                            msg = "审核失败";
                            transaction.Rollback();
                            return false;
                        }
                    }
                    else
                    {
                        msg = "操作异常";
                        transaction.Rollback();
                        return false;
                       
                    }
                    transaction.Commit();
                    msg = "审核成功";
                    return true;
                }
                catch (Exception ex)
                {
                    msg = "审核失败";
                    transaction.Rollback();
                    return false;
                    throw;
                }
            }
        }
    }
}
