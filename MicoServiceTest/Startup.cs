using MicoServiceTest.ExceptionFilterAttrbute;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicoServiceTest
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            // 启用Web API特性路由
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.Filters.Add(new WebApiExceptionFilterAttribute());
            config.Filters.Add(new LogFilterAttribute());

            //出于安全考虑，浏览器会限制脚本中发起的跨站请求，浏览器要求JavaScript或Cookie只能访问同域下的内容
            //开启跨域  
            app.UseCors(CorsOptions.AllowAll);
            
            app.UseWebApi(config);
         
        }
    }
}
