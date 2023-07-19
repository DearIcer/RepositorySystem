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
    public class WorkFlow_ModelBLL : IWorkFlow_ModelBLL
    {
        private RepositorySystemContext _dbContext;
        private IWorkFlow_ModelDAL _workFlow;
        public WorkFlow_ModelBLL(RepositorySystemContext context, IWorkFlow_ModelDAL workFlow )
        { 
            _dbContext = context;
            _workFlow = workFlow;
        }

        public bool CreateWorkFlow_Model(WorkFlow_Model entity, out string msg)
        {
            //throw new NotImplementedException();
            if (string.IsNullOrWhiteSpace(entity.Title))
            {
                msg = "标题不能为空";
                return false;
            }
            WorkFlow_Model workFlow_Model = _workFlow.GetEntities().FirstOrDefault(x => x.Title == entity.Title);   
            if (workFlow_Model != null)
            {
                msg = "标题已存在";
                return false;
            }

            // 赋值id
            entity.Id = Guid.NewGuid().ToString();           
            entity.CreatedTime = DateTime.Now;
            try
            {
                _workFlow.CreateEntity(entity);
                msg = $"添加{entity.Title}成功!";
                return true;
            }
            catch (Exception ex)
            {
                msg = "添加模板失败";
                return false;
            }
        }

        public object GetSelectOptions()
        {
            var list = _workFlow.GetEntities().Where(x => x.IsDelete == false).Select(x => new
            {
                value = x.Id, title = x.Title
            }).ToList();
            return list;
        }

        public List<GetWorkFlow_ModelDTO> GetWorkFlow_Model(int page, int limit, string id, out int count)
        {
            var tempList = (from r in _workFlow.GetWorkFlow_Model().Where(r => r.IsDelete == false)
                            select new GetWorkFlow_ModelDTO
                            {
                                Id = r.Id,
                                Title = r.Title,
                                Description = r.Description,
                                CreateTime = r.CreatedTime
                            }).ToList();
                
            count = tempList.Count;
            return tempList;
        }
    }
}
