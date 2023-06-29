﻿using BLL;
using CommonLib;
using IBLL;
using IDAL;
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
    public class RoleInfoController : Controller
    {
        // GET: RoleInfo
        private RepositorySystemContext _dbContext;
        private IRoleInfoBLL _roleInfo;
        public RoleInfoController(RepositorySystemContext dbcontext , IRoleInfoBLL roleInfo)
        { 
            this._dbContext = dbcontext;
            this._roleInfo = roleInfo;
        }
        public ActionResult ListView()
        {
            return View();
        }
        public ActionResult CreateRoleInfoView()
        {
            return View();
        }
        public ActionResult UpdateRoleInfoView()
        {
            return View();
        }
        /// <summary>
        /// 获取所有的角色表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="id"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public ActionResult GetRoleInfos(int page, int limit, string id, string RoleName)
        {
            int count;

            List<GetRoleInfoDTO> list = _roleInfo.GetAllRoleInfos(page, limit, id, RoleName, out count);

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
        /// 添加角色的接口
        /// </summary>
        /// <param name="role">页面传入的角色实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateRoleInfo([Form] RoleInfo role)
        {
            string msg;

            bool isSuccess = _roleInfo.CreateRoleInfo(role, out msg);

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
        /// 更新角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateRoleInfo([Form] RoleInfo role)
        {
            string msg;

            bool isSuccess = _roleInfo.UpdateRoleInfo(role, out msg);

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
        /// 角色软删除，单实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteRoleInfo(string id)
        {
            ReturnResult result = new ReturnResult();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Msg = "id不能为空";
                return new JsonHelper(result);
            }

            bool isOK = _roleInfo.DeleteRoleInfo(id);

            if (isOK)
            {
                result.Msg = "删除角色成功";
                result.Code = 200;
            }

            return new JsonHelper(result);
        }
        /// <summary>
        /// 角色软删除，多实例
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteRoleInfos(List<string> ids)
        {
            ReturnResult result = new ReturnResult();
            if (ids == null || ids.Count == 0)
            {
                result.Msg = "选中角色为空";
                return new JsonHelper(result);
            }
            bool isOk = _roleInfo.DeleteRoleInfo(ids);
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
    }
}