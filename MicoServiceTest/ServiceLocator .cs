/*
 * 
 * 名称：ServiceLocator
 * 
 * 描述：IOC中继器，使IOC不依赖具体组件
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
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace MicoServiceTest
{
    public sealed class ServiceLocator : IServiceLocator
    {
         
        private readonly IUnityContainer _container;
        private static readonly ServiceLocator _instance = new ServiceLocator();

        private ServiceLocator()
        {
            _container = new UnityContainer();

            //var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            //if (section!=null)
            //    section.Configure(_container);
        }

        public static ServiceLocator Instance
        {
            get { return _instance; }
        }

        public object GetContainer()
        {
            return _container;
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public object GetInstance(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return _container.Resolve(serviceType, key);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }

        public TService GetInstance<TService>()
        {
            return _container.Resolve<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return _container.Resolve<TService>(key);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.ResolveAll<TService>();
        }

        public TService BuildUp<TService>(TService obj)
        {
            return _container.BuildUp<TService>(obj);
        }
    }

    public interface IServiceLocator : IServiceProvider
    {
        object GetInstance(Type serviceType);

        object GetInstance(Type serviceType, string key);

        IEnumerable<object> GetAllInstances(Type serviceType);

        TService GetInstance<TService>();

        TService GetInstance<TService>(string key);

        IEnumerable<TService> GetAllInstances<TService>();
    }
}
