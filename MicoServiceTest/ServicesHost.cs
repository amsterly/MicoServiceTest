/*
 * 
 * 名称：ServicesHost
 * 
 * 描述：服务宿主，所有子服务均是从这里启动
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
using System.Collections.Generic;
using log4net;
using MicoServiceTest.Interface;


namespace MicoServiceTest
{

    public class ServicesHost 
    {
        private readonly ILog _log = LogManager.GetLogger(Log.DefaultLogName);

        public IEnumerable<IService> ServiceList { get; private set; }

        public ServicesHost()
        {
            ServiceList = ServiceLocator.Instance.GetAllInstances<IService>();
        }

        public void Start()
        {
            foreach (var service in ServiceList)
            {
                try
                {
                    service.Start();
                }
                catch (Exception ex)
                {
                    _log.Error(service.GetType().Name + " started error!", ex);
                }
            }
        }

        public void Stop()
        {
            foreach (var service in ServiceList)
            {
                try
                {
                    service.Stop();
                }
                catch (Exception ex)
                {
                    _log.Error(service.GetType().Name + " stoped error!", ex);
                }
            }
        }
    }
}
