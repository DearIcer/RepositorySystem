using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    /// <summary>
    /// 菜单树
    /// </summary>
    public class HomeMenuInfoDTO
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string PId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 节点地址
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 新开Tab方式
        /// </summary>
        public string Target { get; set; } = "_self";

        /// <summary>
        /// 菜单图标样式
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public List<HomeMenuInfoDTO> Child { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Image { get; set; }
    }
}
