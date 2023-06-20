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
        public ActionResult UpdateDepartmentInfoView()
        {
            return View();
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
        /// <summary>
        /// 部门软删除接口
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult DeleteDepartmentInfo(string id)
        {
            ReturnResult result = new ReturnResult();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Msg = "id不能为空";
                return new JsonHelper(result);
            }

            bool isOK = _departmentInfoBLL.DeleteDepartmentInfo(id);

            if (isOK)
            {
                result.Msg = "删除部门成功";
                result.Code = 200;
            }

            return new JsonHelper(result);
        }
        /// <summary>
        /// 部门软删除接口，批量
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteDepartmentInfos(List<string> ids)
        {
            ReturnResult result = new ReturnResult();
            if (ids == null || ids.Count == 0)
            {
                result.Msg = "选中用户为空";
                return new JsonHelper(result);
            }
            bool isOk = _departmentInfoBLL.DeleteDepartmentInfos(ids);
            if (isOk)
            {
                result.Msg = "删除成功";
                result.Code = 200;
            }
            else
            {
                result.Msg = "删除失败";
            }
            return new JsonHelper(result);
        }

        /// <summary>
        /// 更新部门的接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatDepartmentInfo([Form] DepartmentInfo department)
        {
            string msg;

            bool isSuccess = _departmentInfoBLL.UpdateDepartmentInfo(department, out msg);

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