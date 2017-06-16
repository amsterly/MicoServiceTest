/*
 * 
 * 名称：SelfHostService
 * 
 * 描述：Owin WEBAPI子服务
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
using Microsoft.Owin.Hosting;

using MicoServiceTest.Interface;
using Nuctech.MS.WSDemo.SelfHost.Configs;

namespace MicoServiceTest.SelfHost
{
    public class SelfHostService : IService
    {
        private readonly ILog _log = LogManager.GetLogger(Log.DefaultLogName);
        private StartOptions _options;
        private IDisposable _serverDisposable;

        public void Start()
        {
            var url = string.Format("http://*:{0}/", SelfHostConfig.Instance.Port);
            _options = new StartOptions();
            _options.Urls.Add(url);
       
            _serverDisposable = WebApp.Start(_options);
            _log.Info("selfhost service started!");
        }

        public void Stop()
        {
            _serverDisposable.Dispose();
        }
    } 
}
