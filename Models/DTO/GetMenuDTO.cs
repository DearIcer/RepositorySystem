using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class GetMenuDTO
    {
        public GetMenuDTO() { }
        public GetMenuDTO(List<HomeMenuInfoDTO> menus)
        {
            var homeMenusDTO = this.MenuInfo.FirstOrDefault();
            if (homeMenusDTO != null) { homeMenusDTO.Child = menus; }
        }
        /// <summary>
        /// Home
        /// </summary>
        public HomeMenuInfoDTO HomeInfo { get; set; } = new HomeMenuInfoDTO()
        {
            Title = "首页",
            Href = "../layuimini/page/welcome-1.html?t=1"
        };

        /// <summary>
        /// logo
        /// </summary>
        public HomeMenuInfoDTO LogoInfo { get; set; } = new HomeMenuInfoDTO()
        {
            Title = "物资管理系统",
            Image = "../layuimini/images/logo.png",
            Href = ""
        };

        /// <summary>
        /// 权限菜单树
        /// </summary>
        public List<HomeMenuInfoDTO> MenuInfo { get; set; } = new List<HomeMenuInfoDTO>()
        {
            new HomeMenuInfoDTO()
            {
                Title = "常规管理",
                Icon = "fa fa-address-book",
                Href = "",
                Target = "_self"
            }
        };


        
    }
}
