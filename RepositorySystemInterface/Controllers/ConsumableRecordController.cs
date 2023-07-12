using CommonLib;
using IBLL;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositorySystemInterface.Controllers
{
    public class ConsumableRecordController : Controller
    {
        private IConsumableRecordBLL _consumableRecordBLL;
        public ConsumableRecordController(IConsumableRecordBLL consumableRecordBL)
        {
            _consumableRecordBLL = consumableRecordBL;
        }
        // GET: ConsumableRecord
        public ActionResult ListView()
        {
            return View();
        }
        public ActionResult CreatetConsumableRecordView()
        {
            return View();
        }
        public ActionResult GetConsumableRecord(int page, int limit, string id, string name)
        {
            int count;

            List<GetConsumableRecordDTO> list = _consumableRecordBLL.GetConsumableRecordes(page, limit, id, name, out count);

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