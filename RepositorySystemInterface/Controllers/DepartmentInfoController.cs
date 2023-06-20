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
        /// <summary>
        /// 获取所有部门表的接口
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="departmentInfoId"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
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

        public ActionResult CreateDepartmentInfoView()
        {
            return View();
        }

        /// <summary>
        /// 添加部门的接口
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateDepartmentInfo([Form] DepartmentInfo infos)
        {
            string msg;

            bool isSuccess = _departmentInfoBLL.CreateDepartmentInfo(infos,out msg);

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