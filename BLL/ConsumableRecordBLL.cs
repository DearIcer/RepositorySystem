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
            var tempList = (from r in _consumableRecordDAL.GetConsumableRecord()
                            select new GetConsumableRecordDTO
                            {
                                Id = r.Id,
                                ConsumableId = r.ConsumableId,
                                Num = r.Num,
                                Type = r.Type,
                                CreateTime = r.CreatedTime,
                                Creator = r.Creator,
                            }).ToList();
            count = _consumableRecordDAL.GetConsumableRecord().Count();
            return tempList.OrderBy(u => u.Id).Skip(limit * (page - 1)).Take(limit).ToList();
        }

        public bool UpdateConsumableRecord(ConsumableRecord entity, out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
