using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WorkFlow_InstanceStep : BaseEntity
    {
        /// <summary>
        /// 工作流实例Id
        /// </summary>
        [MaxLength(36)]
        public string InstanceId { get; set; }
        /// <summary>
        /// 审核人Id
        /// </summary>
        [MaxLength(36)]
        public string ReviewerId { get; set; }
        /// <summary>
        /// 审核理由
        /// </summary>
        [MaxLength(64)]
        public string ReviewReason { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int ReviewStatus { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ReviewTime { get; set; }
        /// <summary>
        /// 上一个步骤Id
        /// </summary>
        [MaxLength(36)]
        public string BeforeStepId { get; set; }
    }
}
