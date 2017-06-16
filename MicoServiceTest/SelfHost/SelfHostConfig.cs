using System;
using System.Configuration;

namespace Nuctech.MS.WSDemo.SelfHost.Configs
{
    public sealed class SelfHostConfig
    {
        public int Port { get; set; }

        private SelfHostConfig()
        {

        }

        static SelfHostConfig()
        {
            _instance.Port = ConvertAppSettingValue(Convert.ToInt32, ConfigurationManager.AppSettings["WebApiPort"],8701);
        }

        private static readonly SelfHostConfig _instance = new SelfHostConfig();

        public static SelfHostConfig Instance
        {
            get { return _instance; }
        }

        public static T ConvertAppSettingValue<T>(Func<string, T> convertFunc, string appString, T defaultValue)
        {
            if (string.IsNullOrEmpty(appString))
                return defaultValue;

            try
            {
                return convertFunc(appString);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public void Init()
        {
        }
    }
}
