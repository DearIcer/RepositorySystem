using DAL;
using IBLL;
using IDAL;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ConsumableInfoBLL : IConsumableInfoBLL
    {
        private IConsumableInfoDAL _consumableInfoDAL;
        private RepositorySystemContext _dbContext;
        public ConsumableInfoBLL(RepositorySystemContext dbContext, IConsumableInfoDAL consumableInfoDAL) { _dbContext = dbContext; _consumableInfoDAL = consumableInfoDAL; }

        public bool CreateConsumableInfo(ConsumableInfo entity, out string msg)
        {
            //throw new NotImplementedException();
            if (string.IsNullOrWhiteSpace(entity.ConsumableName))
            {
                msg = "耗材名字不能为空";
                return false;
            }


            ConsumableInfo consumableInfo = _consumableInfoDAL.GetEntities().FirstOrDefault(u => u.ConsumableName == entity.ConsumableName);
            if (consumableInfo != null)
            {
                msg = "耗材已存在";
                return false;
            }

            // 赋值id
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedTime = DateTime.Now;
            try
            {
                _consumableInfoDAL.CreateEntity(entity);
                msg = $"添加{entity.ConsumableName}成功!";
                return true;
            }
            catch (Exception ex)
            {
                msg = "添加分类失败";
                return false;
            }
        }

        public List<GetConsumableInfoDTO> GetAllConsumableInfos(int page, int limit, string id, string ConsumableName, out int count)
        {
            var tempList = (from r in _consumableInfoDAL.GetConsumableInfo().Where(r => r.IsDelete == false)
                            select new GetConsumableInfoDTO
                            {
                                Id = r.Id,
                                Description = r.Description,
                                CategoryId = r.CategoryId,
                                ConsumableName = r.ConsumableName,
                                Specification = r.Specification,
                                Num = r.Num,
                                Unit = r.Unit,
                                Money = r.Money,
                                WarningNum = r.WarningNum,
                                CreateTime =    r.CreatedTime

                            }).ToList();
            count = _consumableInfoDAL.GetConsumableInfo().Count();
            return tempList.OrderBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList(); ;
        }

        public object GetSelectOptions()
        {
            throw new NotImplementedException();
        }

        public bool UpdateConsumableInfo(ConsumableInfo entity, out string msg)
        {
            if (string.IsNullOrWhiteSpace(entity.ConsumableName))
            {
                msg = "名字不能为空";
                return false;
            }

            ConsumableInfo consumableInfo = _consumableInfoDAL.GetEntities().FirstOrDefault(u => u.Id == entity.Id);
            if (consumableInfo == null)
            {
                msg = "分类不存在";
                return false;
            }
            consumableInfo.ConsumableName = entity.ConsumableName;
            consumableInfo.Specification = entity.Specification;
            consumableInfo.Unit = entity.Unit;
            consumableInfo.Money = entity.Money;
            consumableInfo.WarningNum = entity.WarningNum;
            consumableInfo.Description = entity.Description;
            consumableInfo.CategoryId = entity.CategoryId;
            try
            {
                _consumableInfoDAL.UpdateEntity(consumableInfo);
                msg = $"更新{consumableInfo.ConsumableName}成功!";
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                msg = "并发更新异常，实体已被修改，请重新加载实体。";
                // 重新加载实体
                _dbContext.Entry(consumableInfo).Reload();
                return false;
            }
            catch (Exception ex)
            {
                msg = "更新分类失败";
                return false;
            }
        }
    }
}
