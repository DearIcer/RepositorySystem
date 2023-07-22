using BLL;
using CommonLib;
using IBLL;
using Models;
using Models.DTO;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace RepositorySystemInterface.Controllers
{
    public class WorkFlow_InstanceController : Controller
    {
        // GET: WorkFlow_Instance
        private IWorkFlow_ModelBLL _workFlow_ModelBLL;
        private IConsumableInfoBLL _consumableInfoBLL;
        private IWorkFlow_InstanceBLL _workFlow_InstanceBLL;

        public WorkFlow_InstanceController(IWorkFlow_ModelBLL workFlow_ModelBLL, IConsumableInfoBLL consumableInfoBLL , IWorkFlow_InstanceBLL workFlow_InstanceBLL) 
        {
            _workFlow_ModelBLL = workFlow_ModelBLL;
            _consumableInfoBLL = consumableInfoBLL;
            _workFlow_InstanceBLL = workFlow_InstanceBLL;
        }
        public ActionResult ListView()
        {
            return View();
        }
        public ActionResult CreatetWorkFlow_InstanceView()
        {
            return View();
        }
        public ActionResult DeleteWorkFlow_InstanceView() { return View(); }
        [HttpGet]
        public ActionResult GetSelectOptions()
        {
            ReturnResult result = new ReturnResult();

            var modelSelect = _workFlow_ModelBLL.GetSelectOptions();
            var consumableInfoSelect = _consumableInfoBLL.GetSelectOptions();

            result.Data = new
            {
                modelSelect,
                consumableInfoSelect
            };
            result.Code = 200;
            result.Msg = "成功!";
            result.IsSuccess = true;
            
            return new JsonHelper(result);
        }
        [HttpPost]
        public ActionResult CreateWorkFlow_Instance([Form] WorkFlow_Instance infos)
        {
            ReturnResult result = new ReturnResult();

            var userId = HttpContext.Request.Cookies["UserId"];
            if (userId == null) 
            {
                result.Msg = "用户id为空";
                return new JsonHelper(result);
            }
            if (string.IsNullOrWhiteSpace(infos.ModelId))
            {
                result.Msg = "工作模板为空";
                return new JsonHelper(result);
            }
            if (string.IsNullOrWhiteSpace(infos.OutGoodsId))
            {
                result.Msg = "物品为空";
                
                return new JsonHelper(result);
            }
            if (infos.OutNum <= 0)
            {
                result.Msg = "申请数量为空";
                return new JsonHelper(result);
            }
            string msg;
            bool isOk = _workFlow_InstanceBLL.CreateWorkFlow_Instance(infos,userId.Value,out msg);
            result.Msg = msg;
            result.IsSuccess = isOk;
            if (isOk)
            {
                result.Code = 200;
            }
            return new JsonHelper(result);
        }
        [HttpGet]
        public ActionResult GetWorkFlow_Instance(int page, int limit)
        {
            int count;
            var userId = HttpContext.Request.Cookies["UserId"];
            List<GetWorkFlow_InstanceDTO> list = _workFlow_InstanceBLL.GetWorkFlow_Instance(page, limit, userId.Value, out count);

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
        
    }
}