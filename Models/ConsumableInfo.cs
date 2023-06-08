using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ConsumableInfo : BaseDeleteEntity
    {
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(32)]
        public string Description { get; set; }
        /// <summary>
        /// 耗材类型id
        /// </summary>
        [MaxLength(36)]
        public string CategoryId { get; set; }
        /// <summary>
        /// 耗材名称
        /// </summary>
        [MaxLength(16)]
        public string ConsumableName { get; set; }
        /// <summary>
        /// 耗材规格
        /// </summary>
        [MaxLength(32)]
        public string Specification { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [MaxLength(8)]
        public string Unit { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 警告库存
        /// </summary>
        public int WarningNum { get; set; }
    }
}
