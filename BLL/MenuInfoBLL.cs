using IBLL;
using IDAL;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class MenuInfoBLL : IMenuInfoBLL
    {
        private RepositorySystemContext _dbContext;
        private IMenuInfoDAL _menuInfoDAL;
        private IR_UserInfo_RoleInfoDAL _r_UserInfo_RoleInfoDAL;
        private IR_RoleInfo_MenuInfoDAL _r_RoleInfo_MenuInfoDAL;
        public MenuInfoBLL(RepositorySystemContext dbContext, IMenuInfoDAL menuInfoDAL, IR_UserInfo_RoleInfoDAL r_UserInfo_RoleInfoDAL, IR_RoleInfo_MenuInfoDAL r_RoleInfo_MenuInfoDAL)
        {
            _dbContext = dbContext;
            _menuInfoDAL = menuInfoDAL;
            this._r_UserInfo_RoleInfoDAL = r_UserInfo_RoleInfoDAL;
            this._r_RoleInfo_MenuInfoDAL = r_RoleInfo_MenuInfoDAL;
        }

        public bool CreateMenuInfo(MenuInfo entity, out string msg)
        {
            //throw new NotImplementedException();
            if (string.IsNullOrWhiteSpace(entity.Title))
            {
                msg = "标题不能为空!";
            }
            if (string.IsNullOrWhiteSpace(entity.Description))
            {
                msg = "描述不能为空!";
            }
            if (string.IsNullOrWhiteSpace(entity.Level.ToString()))
            {
                msg = "等级不能为空!";
            }
            if (string.IsNullOrWhiteSpace(entity.Sort.ToString()))
            {
                msg = "排序不能为空!";
            }
            if (string.IsNullOrWhiteSpace(entity.Href))
            {
                msg = "填写访问地址不能为空!";
            }
            if (string.IsNullOrWhiteSpace(entity.ParentId))
            {
                msg = "父菜单id不能为空!";
            }
            if (string.IsNullOrWhiteSpace(entity.Icon))
            {
                msg = "图标样式不能为空!";
            }
            if (string.IsNullOrWhiteSpace(entity.Target))
            {
                msg = "目标不能为空!";
            }
            MenuInfo info = _menuInfoDAL.GetEntities().FirstOrDefault(u => u.Title == entity.Title);
            if (info != null)
            {
                msg = "菜单已存在";
                return false;
            }
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedTime = DateTime.Now;
            bool isSuccess = _menuInfoDAL.CreateEntity(entity);
            msg = isSuccess ? $"添加{entity.Title}成功!" : "添加菜单失败";
            return isSuccess;
        }

        public bool DeleteMenuInfo(string id)
        {
            //throw new NotImplementedException();
            MenuInfo info = _menuInfoDAL.GetEntities().FirstOrDefault(u => u.Id == id);
            if (info == null)
            {
                return false;
            }
            info.IsDelete = true;
            info.DeleteTime = DateTime.Now;

            return _menuInfoDAL.UpdateEntity(info);
        }

        public bool DeleteMenuInfos(List<string> ids)
        {
            foreach (var item in ids)
            {
                // 根据用户ID查询部门
                MenuInfo info = _menuInfoDAL.GetEntities().FirstOrDefault(u => u.Id == item);
                if (info == null)
                {
                    continue;
                }
                info.IsDelete = true;
                info.DeleteTime = DateTime.Now;

                _menuInfoDAL.UpdateEntity(info);
            }
            return true;
        }

        public List<HomeMenuInfoDTO> GetAllHomeMenuInfos(string userId)
        {
            //throw new NotImplementedException();
            //先获取角色的id
            List<string> roleIds = _r_UserInfo_RoleInfoDAL.GetEntities().Where(u => u.UserId == userId).Select(u => u.RoleId).ToList();
            //通关角色查询可访问的菜单
            List<string> menuIds = _r_RoleInfo_MenuInfoDAL.GetEntities().Where(x => roleIds.Contains(x.RoleId)).Select(x => x.MenuId).ToList();
            //获取当前用户能够访问的菜单集
            List<MenuInfo> allMenus = _menuInfoDAL.GetEntities().Where(x => menuIds.Contains(x.Id)).OrderBy(x => x.Sort).ToList();
            //寻找顶级菜单
            List<HomeMenuInfoDTO> topMenus = allMenus.Where(x => x.Level == 1).OrderBy(x => x.Sort).Select(x => new
            HomeMenuInfoDTO()
            {
                Id = x.Id,
                Title = x.Title,
                Href = x.Href,
                Target = x.Target,
                Icon = x.Icon,
            }).ToList();
            //// 通关遍历查询顶级菜单的子菜单
            //foreach (var item in topMenus)
            //{
            //    List<HomeMenuInfoDTO> childMenus = allMenus.Where(x => x.ParentId == item.Id).Select(x => new HomeMenuInfoDTO()
            //    {
            //        Id = x.Id,
            //        Title = x.Title,
            //        Href = x.Href,
            //        Target = x.Target,
            //        Icon = x.Icon,
            //    }).ToList();
            //    item.Child = childMenus;
            //}
            ///使用递归实现来获取子菜单
            GetChilMenus(topMenus, allMenus);
            return topMenus;
        }

        public void GetChilMenus(List<HomeMenuInfoDTO> parentMenus,List<MenuInfo> allMenus)
        {
            foreach (var item in parentMenus)
            {
                List<HomeMenuInfoDTO> childMenus = allMenus.Where(x => x.ParentId == item.Id).Select(x => new HomeMenuInfoDTO()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Href = x.Href,
                    Target = x.Target,
                    Icon = x.Icon,
                }).ToList();
                // 递归
                GetChilMenus(childMenus, allMenus);
                item.Child = childMenus;
            }
        }

        /// <summary>
        /// 查询菜单列表的函数
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="id"></param>
        /// <param name="MenuName"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<GetMenuInfoDTO> GetAllMenuInfos(int page, int limit, string id, string MenuName, out int count)
        {

            #region 测试显示数据

            //var menuInfo = _menuInfoDAL.GetMenuInfos().Where(r => r.IsDelete == false);

            //count = menuInfo.Count();

            //var listPage = menuInfo.OrderByDescending(u => u.CreatedTime).Skip(limit * (page - 1)).Take(limit).ToList();

            //List<GetMenuInfoDTO> tempList = new List<GetMenuInfoDTO>();

            //foreach (var item in listPage)
            //{
            //    GetMenuInfoDTO data = new GetMenuInfoDTO()
            //    {
            //        Id = item.Id,
            //        Title = item.Title,
            //        Target = item.Target,
            //        Description = item.Description,
            //        Level = item.Level,
            //        Sort = item.Sort,
            //        Href = item.Href,
            //        ParentId = item.ParentId,
            //        Icon = item.Icon,
            //        CreateTime = item.CreatedTime
            //    };
            //    tempList.Add(data);
            //}

            //return tempList;
            #endregion
            #region 实际查询
            //var tempList = (from r in _menuInfoDAL.GetMenuInfos().Where(r => r.IsDelete == false)
            //                join m in _menuInfoDAL.GetMenuInfos().Where(r => r.IsDelete == false)
            //                on r.ParentId equals m.ParentId // 修改join条件
            //                select new GetMenuInfoDTO
            //                {
            //                    Id = r.Id,
            //                    Title = r.Title,
            //                    Target = r.Target,
            //                    Description = r.Description,
            //                    Level = r.Level,
            //                    Sort = r.Sort,
            //                    Href = r.Href,
            //                    ParentId = r.ParentId, // 使用r.ParentId作为父级ID值
            //                    Icon = r.Icon,
            //                    CreateTime = r.CreatedTime
            //                }).ToList();
            //count = tempList.Count;

            var data = from m in _dbContext.MenuInfo.Where(r => r.IsDelete == false)
                       join m2 in _dbContext.MenuInfo.Where(r => r.IsDelete == false)
                       on m.ParentId equals m2.Id
                       into m3
                       from mm in m3.DefaultIfEmpty()
                       select new GetMenuInfoDTO
                       {
                           Id = m.Id,
                           Title = m.Title,
                           Target = m.Target,
                           Description = m.Description,
                           Level = m.Level,
                           Sort = m.Sort,
                           Href = m.Href,
                           ParentId = m.ParentId, // 使用r.ParentId作为父级ID值
                           Icon = m.Icon,
                           CreateTime = m.CreatedTime
                       };
            count = data.Count();
            return data.OrderBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();

            #endregion

        }
        public List<GetMenuInfoDTO> GetAllMenuInfos()
        {
            List<GetMenuInfoDTO> MenuList = _menuInfoDAL.GetEntities().Where(u => u.IsDelete == false)
                                                                      .Select(u => new GetMenuInfoDTO
                                                                      {
                                                                          Id = u.Id,
                                                                          Title = u.Title,
                                                                          Description = u.Description,
                                                                          Level = u.Level,
                                                                          Sort = u.Sort,
                                                                          Href = u.Href,
                                                                          ParentId = u.ParentId,
                                                                          Icon = u.Icon,
                                                                          Target = u.Target,
                                                                          CreateTime = u.CreatedTime
                                                                      })
                                                                      .ToList();
            return MenuList;
        }

        /// <summary>
        /// 根据id获取菜单表
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public MenuInfo GetMenuInfoById(string id)
        {
            return _menuInfoDAL.GetEntityByID(id);
        }

        public object GetSelectOptions()
        {
            var parentSelect = _dbContext.MenuInfo.Where(m => m.IsDelete == false).Select(m => new
            {
                value = m.Id,
                title = m.Title
            }).ToList();

            var data = new
            {
                parentSelect
            };
            return data;
        }

        public bool UpdateMenuInfo(MenuInfo entity, out string msg)
        {
            if (entity == null) { msg = "数据参数为空！"; }
            if(entity.Title == null) { msg = "标题不能为空"; }
            MenuInfo menu = _menuInfoDAL.GetEntityByID(entity.Id);
            if (menu == null) { msg = "菜单数据为空！"; return false; }
            menu.Title = entity.Title;
            menu.Description = entity.Description;  
            menu.Icon = entity.Icon;
            menu.Target = entity.Target;
            //menu.CreatedTime = entity.CreatedTime;
            menu.Href = entity.Href;
            menu.ParentId = entity.ParentId;
            menu.Level = entity.Level;
            menu.Sort = entity.Sort;
            
            bool isOk = _menuInfoDAL.UpdateEntity(menu);

            msg = isOk ? $"修改{entity.Title}成功!" : "添加修改失败";

            return isOk;

        }
    
    }
}
