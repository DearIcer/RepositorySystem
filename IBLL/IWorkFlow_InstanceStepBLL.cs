using Models.DTO;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enums;

namespace IBLL
{
    public interface IWorkFlow_InstanceStepBLL
    {
        /// <summary>
        /// 添加实例数据（申领记录）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CreateWorkFlow_InstanceStep(WorkFlow_InstanceStepDTO entity, string userId, out string msg);

        /// <summary>
        /// 获取当前用户所有的申请记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<WorkFlow_InstanceStepDTO> GetWorkFlow_InstanceStep(int page, int limit, string userId, out int count);

        bool UpdateWorkFlow_InstanceStep(string id, int outNum, string reviewReason, string userId, WorkFlow_InstanceStepStatusEnum workFlow_InstanceStepStatusEnum, out string msg);
    }
}
