using CommonLib;
using IBLL;
using IDAL;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace BLL
{
    public class CategoryBLL : ICategoryBLL
    {
        private ICategoryDAL _categoryDAL;
        private RepositorySystemContext _dbContext;
        public CategoryBLL(RepositorySystemContext dbContext, ICategoryDAL categoryDAL) { _dbContext = dbContext; _categoryDAL = categoryDAL; }
        public bool CreateCategory(Category entity, out string msg)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (ParameterHelper.ValidateParameter(entity.Description, "描述", out msg) &&
                       ParameterHelper.ValidateParameter(entity.CategoryName, "分类名", out msg)
                       )
                    {
                        Category category = _categoryDAL.GetEntities().FirstOrDefault(u => u.CategoryName == entity.CategoryName);
                        if (category != null)
                        {
                            msg = "分类已存在";
                            return false;
                        }
                        // 赋值id
                        entity.Id = Guid.NewGuid().ToString();
                        entity.CreatedTime = DateTime.Now;
                        _dbContext.Category.Add(entity);
                        _dbContext.SaveChanges();   
                        transaction.Commit();
                        msg = $"添加{entity.CategoryName}成功!";
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    transaction.Rollback();
                    return false;
                    throw;
                }
            }

        }

        public bool DeleteCategory(string id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Category category = _categoryDAL.GetEntities().FirstOrDefault(u => u.Id == id);
                    if (category == null)
                    {
                        return false;
                    }
                    category.IsDelete = true;
                    category.DeleteTime = DateTime.Now;
                    _categoryDAL.UpdateEntity(category);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                    throw;
                }
            }

        }

        public bool DeleteCategory(List<string> ids)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in ids)
                    {
                        Category category = _categoryDAL.GetEntities().FirstOrDefault(u => u.Id == item);
                        if (category == null)
                        {
                            continue;
                        }
                        category.IsDelete = true;
                        category.DeleteTime = DateTime.Now;

                        _categoryDAL.UpdateEntity(category);
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                    throw;
                }
            }

        }


        public List<GetCategoryDTO> GetCategories(int page, int limit, string id, string name, out int count)
        {
            var tempList = (from r in _categoryDAL.GetCatgory().Where(r => r.IsDelete == false)
                            select new GetCategoryDTO
                            {
                                Description = r.Description,
                                CategoryName = r.CategoryName,
                                Id = r.Id,
                            }).ToList();
            count = _categoryDAL.GetCatgory().Count();
            return tempList.OrderBy(u => u.Id).Skip(limit * (page - 1)).Take(limit).ToList(); 
        }

        public List<GetCategoryDTO> GetCategories()
        {
            List<GetCategoryDTO> list = _categoryDAL.GetEntities().Where(r => r.IsDelete == false)
                .Select(u => new GetCategoryDTO
            {
                Description = u.Description,
                CategoryName = u.CategoryName,
                Id = u.Id,
            }).ToList();

            return list;
        }

        public object GetSelectOptions()
        {
            var parentSelect = _dbContext.Category.Where(r => r.IsDelete == false).Select(d => new
            {
                value = d.Id,
                title = d.CategoryName
            }).ToList();
            return parentSelect;
        }


        public bool UpdateCategory(Category entity, out string msg)
        {

            if (ParameterHelper.ValidateParameter(entity.Description, "描述", out msg) &&
               ParameterHelper.ValidateParameter(entity.CategoryName, "分类名", out msg)
               )
            {
                Category category = _categoryDAL.GetEntities().FirstOrDefault(u => u.Id == entity.Id);
                if (category == null)
                {
                    msg = "分类不存在";
                    return false;
                }

                category.Description = entity.Description;
                category.CategoryName = entity.CategoryName;
                try
                {
                    _categoryDAL.UpdateEntity(category);
                    msg = $"更新{category.CategoryName}成功!";
                    return true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    msg = "并发更新异常，实体已被修改，请重新加载实体。";
                    // 重新加载实体
                    _dbContext.Entry(category).Reload();
                    return false;
                }
                catch (Exception ex)
                {
                    msg = "更新分类失败";
                    return false;
                }
            }
            else
            {
                msg = string.Empty;
                return false;
            }

        }

    }
}
