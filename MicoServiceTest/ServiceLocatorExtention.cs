/*
 * 
 * 名称：ServiceLocatorExtention
 * 
 * 描述：ServiceLocator扩展类
 * 
 * 创建人：zhaopeng
 * 
 * 创建日期：2016.6.23
 * 
 * 修改人：
 * 
 * 修改日期： 
 * 
 * 版权：
 * 
 * 版本号：
 * 
 */

using System;
using log4net;
using Microsoft.Practices.Unity;

using Nuctech.MS.WSDemo.SelfHost;
using MicoServiceTest.Interface;
using MicoServiceTest.SelfHost;

namespace MicoServiceTest
{
    /// <summary>
    /// ServiceLocator扩展类
    /// </summary>
    public static class ServiceLocatorExtention
    {
        private static readonly ILog _log = LogManager.GetLogger(Log.DefaultLogName);

        /// <summary>
        /// 初始化Unity
        /// </summary>
        /// <param name="serviceLocator"></param>
        public static void Init(this ServiceLocator serviceLocator)
        {
            var container = (IUnityContainer)serviceLocator.GetContainer();

            if (container == null)
            {
                _log.Error("unity container init error: container is null");
                return;
            }

            container
                //    .RegisterType<IWsDevice, WsDevice>()
                //    .RegisterType<IWeatherStore, WeatherDataStoreCenter>()
                //    .RegisterType<IWeatherStore, WeatherCacheStore>("WeatherCacheStore",new ContainerControlledLifetimeManager())
                //    .RegisterType<IRainStore, RainDataStoreCenter>()
                //    .RegisterType<IRainStore, RainCacheStore>("RainCacheStore", new ContainerControlledLifetimeManager())
                //    .RegisterType<IService, MiddlewareService>("MiddlewareService")
                //    .RegisterType<IService, LocalService>("LocalService")
                .RegisterType<IService, SelfHostService>("SelfHostService");
            //    .RegisterType<IService, RainMoniteService>("RainMoniteService");

            //container.RegisterInstance(new RainDataStack(), new ContainerControlledLifetimeManager());
        }
    }

    public static class DateTimeExtention
    {
        public static DateTime ToNoMillisecondDateTime(this DateTime dateTime)
        {
            return DateTime.Parse(dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public static class Log
    {
        public static string DefaultLogName
        {
            get { return "applicationlog"; }
        }
    }
}
