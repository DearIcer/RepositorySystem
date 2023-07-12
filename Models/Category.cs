using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Category : BaseDeleteEntity
    {
        ///// <summary>
        ///// 主键Id
        ///// </summary>
        //[Key]
        //[MaxLength(36)]
        //public string Id { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        [MaxLength(16)]
        public string CategoryName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(32)]
        public string Description { get; set; }
    }
}
