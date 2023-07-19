using BLL;
using CommonLib;
using IBLL;
using Models;
using Models.DTO;
using RepositorySystemInterface.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace RepositorySystemInterface.Controllers
{
    [CustomAttribute]
    public class WorkFlow_ModelController : Controller
    {
        // GET: WorkFlow_Model  GetAllWorkFlow_Model
        private IWorkFlow_ModelBLL _workFlow_ModelBLL;
        public WorkFlow_ModelController(IWorkFlow_ModelBLL workFlow_ModelBLL)
        {
            _workFlow_ModelBLL = workFlow_ModelBLL;
        }

        public ActionResult ListView()
        {
            return View();
        }
        public ActionResult CreatetWorkFlow_ModelView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllWorkFlow_Model(int page, int limit, string id)
        {
            int count;

            List<GetWorkFlow_ModelDTO> list = _workFlow_ModelBLL.GetWorkFlow_Model(page, limit, id , out count);

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
        public ActionResult CreateWorkFlow_Model([Form] WorkFlow_Model infos)
        {
            string msg;

            bool isSuccess = _workFlow_ModelBLL.CreateWorkFlow_Model(infos, out msg);

            ReturnResult result = new ReturnResult();

            result.Msg = msg;

            result.IsSuccess = isSuccess;

            if (isSuccess)
            {
                result.Code = 200;
            }
            return new JsonHelper(result);
        }


    }
}