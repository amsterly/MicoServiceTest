using log4net.Config;
using Nuctech.MS.WSDemo;
using Nuctech.MS.WSDemo.SelfHost.Configs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace MicoServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //initialize log4net
            InitLog4Net();

            //initialize unity
            ServiceLocator.Instance.Init();

            //initialize config
            InitConfig();
            HostFactory.Run(x =>
            {
                x.Service<ServicesHost>(s =>
                {
                    s.ConstructUsing(name => new ServicesHost());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                //x.UseLog4Net();
                x.RunAsLocalSystem();

                x.SetDescription(AppConfig.Instance.Description);
                x.SetDisplayName(AppConfig.Instance.DisplayName);
                x.SetServiceName(AppConfig.Instance.ServiceName);
            });
        }


        private static void InitLog4Net()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Configs/log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }

        public static void InitConfig()
        {
            AppConfig.Instance.Init();
            //LocalConfig.Instance.Init();
            SelfHostConfig.Instance.Init();
            //MiddleWareConfig.Instance.Init();
        }
    }
}
