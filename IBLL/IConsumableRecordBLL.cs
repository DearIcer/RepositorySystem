using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IConsumableRecordBLL
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
        List<GetConsumableRecordDTO> GetConsumableRecordes(int page, int limit, string id, string name, out int count);


        /// <summary>
        /// 添加分类数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CreateConsumableRecord(ConsumableRecord entity, out string msg);

        /// <summary>
        /// 更新分类数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool UpdateConsumableRecord(ConsumableRecord entity, out string msg);

    }
}
