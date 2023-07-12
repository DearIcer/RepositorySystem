using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ICategoryBLL
    {
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<GetCategoryDTO> GetCategories(int page, int limit, string id, string name, out int count);

        /// <summary>
        /// 返回分类列表，非分页
        /// </summary>
        /// <returns></returns>
        List<GetCategoryDTO> GetCategories();

        /// <summary>
        /// 添加分类数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CreateCategory(Category entity , out string msg);

        /// <summary>
        /// 更新分类数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool UpdateCategory(Category entity, out string msg);

        bool DeleteCategory(string id);

        bool DeleteCategory(List<string> ids);
    }
}
