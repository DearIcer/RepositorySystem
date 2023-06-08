using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserInfo : BaseDeleteEntity
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        [MaxLength(16)]
        public string Account { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(16)]
        public string UserName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [MaxLength(16)]
        public string PhoneNum { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(32)]
        public string Email { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        [MaxLength(36)]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(32)]
        public string PassWord { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
