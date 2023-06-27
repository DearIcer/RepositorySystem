using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    /// <summary>
    /// 获取返回部门表的实体类
    /// </summary>
    public class GetDepartmentInfoDTO
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentInfoId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 部门名字
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 主管ID
        /// </summary>
        public string LeaderId { get; set; }
        /// <summary>
        /// 父部门ID
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        public string ParentName { get; set; }
        public string LeaderName { get; set; }
    }
}
