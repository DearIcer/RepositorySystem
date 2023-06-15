using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class ReturnResult
    {
        public int Status { get; set; } = 501;//错误参数

        public string Msg { get; set; } = "失败";
        public bool IsSuccess { get; set; }
        public object Datas { get; set; }
    }
}
