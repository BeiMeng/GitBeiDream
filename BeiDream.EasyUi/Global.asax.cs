using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Util;
using Util.Logs;
using Util.Logs.Log4;

namespace BeiDream.EasyUi
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        /// <summary>
        /// 应用程序错误处理
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
            WriteLog(lastError);
            Server.ClearError();
        }

        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="exception">异常</param>
        private void WriteLog(Exception exception)
        {
            ILog log = Log.GetContextLog(this.GetType());
            log.Application = "管理系统";
            log.Caption.Add("管理后台Global全局异常捕获");
            log.Exception = exception;
            Warning.WriteLog(log, exception);
        }
    }
}