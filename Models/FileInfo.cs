using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FileInfo : BaseEntity
    {
        /// <summary>
        /// 关系id
        /// </summary>
        [MaxLength(36)]
        public string RelationId { get; set; }
        /// <summary>
        /// 原文件名
        /// </summary>
        [MaxLength(32)]
        public string OldFileName { get; set; }
        /// <summary>
        /// 新文件名
        /// </summary>
        [MaxLength(32)]
        public string NewFileName { get; set; }
        /// <summary>
        /// 拓展名
        /// </summary>
        [MaxLength(12)]
        public string Extension { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// 添加人Id
        /// </summary>
        [MaxLength(36)]
        public string Creator { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public int Category { get; set; }
    }
}
