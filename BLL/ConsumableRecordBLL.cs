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
    public class ConsumableRecordBLL : IConsumableRecordBLL
    {
        private RepositorySystemContext _dbContext;
        private IConsumableRecordDAL _consumableRecordDAL;
        public ConsumableRecordBLL(RepositorySystemContext dbContext, IConsumableRecordDAL consumableRecordDAL)
        {
            _dbContext = dbContext;
            _consumableRecordDAL = consumableRecordDAL;
        }

        public bool CreateConsumableRecord(ConsumableRecord entity, out string msg)
        {
            throw new NotImplementedException();
        }

        public List<GetConsumableRecordDTO> GetConsumableRecordes(int page, int limit, string id, string name, out int count)
        {
            //查找日志
            //var tempList = (from r in _consumableRecordDAL.GetConsumableRecord()
            //                select new GetConsumableRecordDTO
            //                {
            //                    Id = r.Id,
            //                    ConsumableId = r.ConsumableId,
            //                    Num = r.Num,
            //                    Type = r.Type,
            //                    CreateTime = r.CreatedTime,
            //                    Creator = r.Creator,
            //                }).ToList();
            //count = _consumableRecordDAL.GetConsumableRecord().Count();
            var tempList = from cr in _dbContext.ConsumableRecord.Where(it => it.Id != null)
                           join u in _dbContext.UserInfo
                           on cr.Creator equals u.Id
                           into CR_U
                           from cru in CR_U.DefaultIfEmpty()//链接用户表用于查询是谁添加的

                           join c in _dbContext.ConsumableInfo
                           on cr.ConsumableId equals c.Id
                           into Cru_U
                           from ccu in Cru_U.DefaultIfEmpty()
                           select new GetConsumableRecordDTO
                           {
                               Id = cr.Id,
                               ConsumableId = cr.ConsumableId,
                               ConsumableName = ccu.ConsumableName,
                               Num = cr.Num,
                               Type = cr.Type == 1 ? "入库" : "出库",
                               CreateTime = cr.CreatedTime,
                               Creator = cr.Creator,
                               CreatorName = cru.UserName,

                           };
            count = _consumableRecordDAL.GetConsumableRecord().Count();
            return tempList.OrderByDescending(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();
        }

        public bool UpdateConsumableRecord(ConsumableRecord entity, out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
