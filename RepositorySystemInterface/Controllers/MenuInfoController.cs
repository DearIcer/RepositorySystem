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
    public class MenuInfoController : Controller
    {

        // GET: Menu
        private IMenuInfoBLL _menuInfoBLL;
        public MenuInfoController(IMenuInfoBLL menuInfo ) 
        {
            _menuInfoBLL = menuInfo;
        }
        public ActionResult ListView()
        {
            return View();
        }

        public ActionResult UpdateMenuInfoView()
        {
            return View();
        }

        public ActionResult CreateMenuInfoView()
        {
            return View();
        }
        //(int page, int limit, string id, string MenuName, out int count);
        [HttpGet]
        public ActionResult GetMenuInfos(int page, int limit, string id, string MenuName)
        {

            int count;

            List<GetMenuInfoDTO> list = _menuInfoBLL.GetAllMenuInfos(page, limit, id, MenuName, out count);

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


        /// <summary>
        /// 添加菜单的接口
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateMenuInfo([Form] MenuInfo infos)
        {
            string msg;

            bool isSuccess = _menuInfoBLL.CreateMenuInfo(infos, out msg);

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
        /// 菜单软删除接口
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult DeleteMenuInfo(string id)
        {
            ReturnResult result = new ReturnResult();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Msg = "id不能为空";
                return new JsonHelper(result);
            }

            bool isOK = _menuInfoBLL.DeleteMenuInfo(id);

            if (isOK)
            {
                result.Msg = "删除菜单成功";
                result.Code = 200;
            }

            return new JsonHelper(result);
        }
        /// <summary>
        /// 菜单软删除接口，批量
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteMenuInfos(List<string> ids)
        {
            ReturnResult result = new ReturnResult();
            if (ids == null || ids.Count == 0)
            {
                result.Msg = "选中菜单为空";
                return new JsonHelper(result);
            }
            bool isOk = _menuInfoBLL.DeleteMenuInfos(ids);
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
        /// 根据用户登录获取访问菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetMenus() 
        {
            //返回的菜单消息
            GetMenuDTO res = new GetMenuDTO();
            #region 测试数据
            //List<HomeMenuInfoDTO> menuInfoList = new List<HomeMenuInfoDTO>()
            //{
            //    new HomeMenuInfoDTO()
            //    {
            //        Title = "用户管理",
            //        Href = "/UserInfo/ListView",
            //        Icon = "fa fa-user",
            //        Target = "_self"
            //    },
            //    new HomeMenuInfoDTO()
            //    {
            //        Title = "系统管理",
            //        Href = "",
            //        Icon =  "fa fa-cog",
            //        Target = "_self",
            //        Child = new List<HomeMenuInfoDTO>
            //        {
            //            new HomeMenuInfoDTO()
            //            {
            //                Title = "角色管理",
            //                Href = "/RoleInfo/ListView",
            //                Icon = "fa fa-street-view",
            //                Target = "_self"
            //            }
            //        }
            //    }
            //};
            //res.MenuInfo[0].Child = menuInfoList;
            #endregion
            //构建菜单树
            var userId = HttpContext.Request.Cookies["UserId"];
            if (userId == null)
            {
                res = new GetMenuDTO(new List<HomeMenuInfoDTO>());
                return new JsonHelper(res);
            }
            else
            {
                List<HomeMenuInfoDTO> menuInfoList = _menuInfoBLL.GetAllHomeMenuInfos(userId.Value);
                res = new GetMenuDTO(menuInfoList);
            }
            //res = new GetMenuDTO(menuInfoList);
            return new JsonHelper(res);
        }
    }
}