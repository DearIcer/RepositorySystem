using DAL;
using IBLL;
using IDAL;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MenuInfoBLL : IMenuInfoBLL
    {
        private RepositorySystemContext _dbContext;
        private IMenuInfoDAL _menuInfoDAL;
        public MenuInfoBLL(RepositorySystemContext dbContext, IMenuInfoDAL menuInfoDAL)
        {
            _dbContext = dbContext;
            _menuInfoDAL = menuInfoDAL;
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
            var tempList = (from r in _menuInfoDAL.GetMenuInfos().Where(r => r.IsDelete == false)
                            join m in _menuInfoDAL.GetMenuInfos().Where(r => r.IsDelete == false)
                            on r.ParentId equals m.ParentId
                                //orderby r.CreatedTime descending
                            select new GetMenuInfoDTO
                            {
                                Id = r.Id,
                                Title = r.Title,
                                Target = r.Target,
                                Description = r.Description,
                                Level = r.Level,
                                Sort = r.Sort,
                                Href = r.Href,
                                ParentId = m.ParentId,
                                Icon = r.Icon,
                                CreateTime = r.CreatedTime
                            }).ToList();
            count = tempList.Count;
            return tempList.OrderBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();
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
    }
}
