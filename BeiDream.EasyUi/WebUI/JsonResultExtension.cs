using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BeiDream.EasyUi.WebUI
{
    /// <summary>
    /// 扩展的jsonResult模型,适用于extjs需要的json数据类型
    /// </summary>
    public class JsonResultExtension:ActionResult
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
        public long? Total { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        /// <summary>
        /// 是否序列化为extjs需要的json格式，否则进行普通序列化
        /// </summary>
        public bool ExtjsUiSerialize { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/json";

            StringWriter sw = new StringWriter();
            //IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            //timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter {DateTimeFormat = "yyyy-MM-dd"};
            JsonSerializer serializer = JsonSerializer.Create(
                new JsonSerializerSettings
                {
                    Converters = new JsonConverter[] { timeFormat },
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore    //忽略为null的值序列化

                }
                );


            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = Formatting.Indented;

                serializer.Serialize(jsonWriter, ExtjsUiSerialize ? this : Data);
            }
            response.Write(sw.ToString());

        }
    }
}