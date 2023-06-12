using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonLib
{
    public class JsonHelper : JsonResult
    {
        public new Object Data { get; set; }
        
        public JsonHelper(object data)
        {
            Data = data;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if(context == null)
            {
                throw new ArgumentException("请求的context");
            }
            //获取响应对象
            var response = context.HttpContext.Response;
            // 设置相对应对象的类型
            response.ContentType = !string.IsNullOrEmpty(response.ContentType) ? response.ContentType : "application/json";
            //编码判断
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            var camel = new JsonSerializerSettings
            {
                //重写驼峰命名
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };

            response.Write(JsonConvert.SerializeObject(Data,camel));  
        }
    }
}
