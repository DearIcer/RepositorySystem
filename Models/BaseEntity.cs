using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 数据库基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime{ get; set; }
    }
}
