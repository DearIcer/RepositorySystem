using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IConsumableInfoBLL
    {
        /// <summary>
        /// 查询耗材列表的函数
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="limit">每页几条数据</param>
        /// <param name="id">ID</param>
        /// <param name="ConsumableName">耗材名</param>
        /// <param name="count">返回的数据总量</param>
        /// <returns>返回耗材数据的列表</returns>
        List<GetConsumableInfoDTO> GetAllConsumableInfos(int page, int limit, string id, string ConsumableName, out int count);

        /// <summary>
        /// 添加耗材数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CreateConsumableInfo(ConsumableInfo entity, out string msg);

        /// <summary>
        /// 更新耗材数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool UpdateConsumableInfo(ConsumableInfo entity, out string msg);

        /// <summary>
        /// 获取下拉列表数据
        /// </summary>
        /// <returns></returns>
        object GetSelectOptions();

        /// <summary>
        /// 上传excel的数据接口
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="extension"></param>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool Upload(Stream stream, string extension, string id, out string msg);

        /// <summary>
        /// 删除耗材数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteConsumableInfo(string id);

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteConsumableInfo(List<string> ids);

        /// <summary>
        /// 返回Excel的数据表
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        Stream GetDownload(out string FileName);

    }
}
