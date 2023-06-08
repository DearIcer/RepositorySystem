using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class R_RoleInfo_MenuInfo : BaseEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [MaxLength(36)]
        public string RoleId { get; set; }
        /// <summary>
        /// 菜单id
        /// </summary>
        [MaxLength(36)]
        public string MenuId { get; set; }
    }
}
