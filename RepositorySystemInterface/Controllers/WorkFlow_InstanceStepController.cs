using CommonLib;
using IBLL;
using Models.DTO;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositorySystemInterface.Controllers
{
    public class WorkFlow_InstanceStepController : Controller
    {
        // GET: WorkFlow_InstanceStep
        private IWorkFlow_InstanceStepBLL _workFlow_InstanceStepBLL;
        public WorkFlow_InstanceStepController(IWorkFlow_InstanceStepBLL workFlow_InstanceStepBLL)
        {
            _workFlow_InstanceStepBLL = workFlow_InstanceStepBLL;
        }

        public ActionResult ListView()
        {
            return View();
        }
        public ActionResult UpdateWorkFlow_InstanceStepView()
        {
            return View();
        }
        public ActionResult GetWorkFlow_InstanceStep(int page, int limit)
        {
            int count;
            var userId = HttpContext.Request.Cookies["UserId"];
            List<WorkFlow_InstanceStepDTO> list = _workFlow_InstanceStepBLL.GetWorkFlow_InstanceStep(page, limit, userId.Value, out count);

            ReturnResult result = new ReturnResult()
            {
                Code = 0,
                Msg = "获取成功",
                Data = list,
                IsSuccess = true,
                Count = count
            };

            return new JsonHelper(result);
        }
        [HttpPost]
        public ActionResult UpdateWorkFlow_InstanceStatus(string id, int outNum, string reviewReason, WorkFlow_InstanceStepStatusEnum reviewStatus)
        {
            var result = new ReturnResult();
            var userId = HttpContext.Request.Cookies["UserId"];
            if (userId == null)
            {
                result.Msg = "登录状态过期";
                return new JsonHelper(result);
            }
            if (string.IsNullOrWhiteSpace(id))
            {
                result.Msg = "id为空";
                return new JsonHelper(result);
            }
            if (string.IsNullOrWhiteSpace(reviewReason))
            {
                result.Msg = "审核意见不能为空";
                return new JsonHelper(result);
            }
            if (outNum < 0)
            {
                result.Msg = "申请数量不能小于零或者对于零";
                return new JsonHelper(result);
            }
            if (reviewStatus != WorkFlow_InstanceStepStatusEnum.同意 && reviewStatus != WorkFlow_InstanceStepStatusEnum.驳回)
            {
                result.Msg = "审核状态错误";
                return new JsonHelper(result);
            }

            string msg;
            result.IsSuccess = _workFlow_InstanceStepBLL.UpdateWorkFlow_InstanceStep(id, outNum, reviewReason, userId.Value, reviewStatus, out msg);
            result.Msg = msg;
            result.Code = result.IsSuccess ? 200 : result.Code;

            return new JsonHelper(result);
        }
    }
}