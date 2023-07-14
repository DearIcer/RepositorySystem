using BLL;
using CommonLib;
using IBLL;
using Models;
using Models.DTO;
using RepositorySystemInterface.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace RepositorySystemInterface.Controllers
{
    [CustomAttribute]
    public class ConsumableInfoController : Controller
    {
        private IConsumableInfoBLL _consumableInfoBLL;
        private ICategoryBLL _categoryBLL;
        public ConsumableInfoController(IConsumableInfoBLL consumableInfoBLL, ICategoryBLL categoryBLL)
        {
            this._consumableInfoBLL = consumableInfoBLL;
            _categoryBLL = categoryBLL;
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
        
        public ActionResult Upload(HttpPostedFileBase file)
        {
            ReturnResult result = new ReturnResult();
            if(file == null)
            {
                result.Msg = "文件为空";
                return new JsonHelper(result);
            }
            string extension = Path.GetExtension(file.FileName);//取文件後綴
            var UserId = HttpContext.Request.Cookies["UserId"];
            if(UserId == null || string.IsNullOrWhiteSpace(UserId.Value))
            {
                result.Msg = "上传用户的ID不存在";
                return new JsonHelper(result);
            }

            Stream stream = file.InputStream;
            string msg;
            bool success = false;
            switch (extension)
            {
                case ".xls":
                    {
                        success = _consumableInfoBLL.Upload(stream,extension,UserId.Value,out msg);
                        break;
                    }                   
                case ".xlsx":
                    {
                        success = _consumableInfoBLL.Upload(stream, extension, UserId.Value, out msg);
                        break;
                    }                  
                default:
                    {
                        result.Code = 501;
                        result.Msg = "上传的文件只能是Excel类型";
                        return new JsonHelper(result);
                    }
                   
            }
            if (success)
            {
                result.Msg = "上传成功";
                result.Code = 200;
                return new JsonHelper(result);
            }
            else
            {
                result.Msg = "上传失败";
                result.Code = 501;
                return new JsonHelper(result);
            }
        }

        /// <summary>
        /// 获取下拉列表的接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSelectOptions()
        {
            ReturnResult result = new ReturnResult();

            var data = _categoryBLL.GetSelectOptions();

            result.Code = 200;
            result.Msg = "获取成功";
            result.IsSuccess = true;
            result.Data = data;

            return new JsonHelper(result);
        }

        public ActionResult DeletetCategory(string id)
        {
            ReturnResult result = new ReturnResult();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Msg = "id不能为空";
                return new JsonHelper(result);
            }

            bool isOK = _consumableInfoBLL.DeleteConsumableInfo(id);

            if (isOK)
            {
                result.Msg = "删除耗材成功";
                result.Code = 200;
            }

            return new JsonHelper(result);
        }
        public ActionResult DeletetCategories(List<string> ids)
        {
            ReturnResult result = new ReturnResult();

            if (ids == null || ids.Count == 0)
            {
                result.Msg = "id不能为空";
                return new JsonHelper(result);
            }

            bool isOK = _consumableInfoBLL.DeleteConsumableInfo(ids);

            if (isOK)
            {
                result.Msg = "删除耗材成功";
                result.Code = 200;
            }

            return new JsonHelper(result);
        }
        public ActionResult DownLoadFile()
        {
            string downloadFileName;
            Stream stream = _consumableInfoBLL.GetDownload(out downloadFileName);
            return File(stream, "application/octet-stream", downloadFileName);
        }
    }
}