using BLL;
using CommonLib;
using IBLL;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace RepositorySystemInterface.Controllers
{
    
    public class ConsumableInfoController : Controller
    {
        private IConsumableInfoBLL _consumableInfoBLL;
        public ConsumableInfoController(IConsumableInfoBLL consumableInfoBLL)
        {
            this._consumableInfoBLL = consumableInfoBLL;
        }
        public ActionResult UpdateConsumableInfoView()
        {
            return View();
        }
        public ActionResult CreatetConsumableInfoView()
        {
            return View();
        }
        public ActionResult ListView()
        {
            return View();
        }
        public ActionResult GetAllConsumableInfos(int page, int limit, string id, string ConsumableName)
        {
            int count;

            List<GetConsumableInfoDTO> list = _consumableInfoBLL.GetAllConsumableInfos(page, limit, id, ConsumableName, out count);

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
        public ActionResult CreateConsumableInfo([Form] ConsumableInfo infos)
        {
            string msg;

            bool isSuccess = _consumableInfoBLL.CreateConsumableInfo(infos, out msg);

            ReturnResult result = new ReturnResult();

            result.Msg = msg;

            result.IsSuccess = isSuccess;

            if (isSuccess)
            {
                result.Code = 200;
            }
            return new JsonHelper(result);
        }

        [HttpPost]
        public ActionResult UpdateConsumableInfo([Form] ConsumableInfo infos)
        {
            string msg;

            bool isSuccess = _consumableInfoBLL.UpdateConsumableInfo(infos, out msg);

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