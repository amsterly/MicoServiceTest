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
                routeTemplate: "api/device/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Filters.Add(new LogFilterAttribute());
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}
