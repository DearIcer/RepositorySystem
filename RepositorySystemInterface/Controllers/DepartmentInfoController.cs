using CommonLib;
using IBLL;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositorySystemInterface.Controllers
{
    public class DepartmentInfoController : Controller
    {
        /// <summary>
        /// 部门表数据
        /// </summary>
        private IDepartmentInfoBLL _departmentInfoBLL;
        public DepartmentInfoController(IDepartmentInfoBLL departmentInfoBLL)
        {
            _departmentInfoBLL = departmentInfoBLL;
        }

        // GET: DepartmentInfo
        public ActionResult ListView()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetDepartmentInfos(int page, int limit, string departmentInfoId, string departmentName)
        {

            int count;

            List<GetDepartmentInfoDTO> list = _departmentInfoBLL.GetDepartmentInfos(page,limit,departmentInfoId,departmentName,out count);

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