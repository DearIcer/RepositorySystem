using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class WorkFlow_InstanceStepDTO
    {
        public string Id { get; set; }
        public string InstanceId { get; set; }
        public string ModelTitle { get; set; }
        public string InstanceName { get; set; }
        public string Reason { get; set; }
        public string ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewReason { get; set; }
        public int ReviewStatus{ get; set; }
        public DateTime? ReviewTime { get; set; }
        public string BeforeStepId { get; set; }
        public string CreatorName { get; set; }
        public string CreatorId { get; set; }
        public int OutNum { get; set; }
        public string OutGoodsName { get; set; }
        public DateTime CreateTime{ get; set; }

    }
}
