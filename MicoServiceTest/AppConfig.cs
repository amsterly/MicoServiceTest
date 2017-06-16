using System;
using System.Configuration;

namespace Nuctech.MS.WSDemo
{
    public sealed class AppConfig
    {
        public string ServiceName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public void Init()
        {
        }

        private AppConfig()
        {

        }

        static AppConfig()
        {
            _instance.ServiceName = ConfigurationManager.AppSettings["ServiceName"];
            _instance.DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            _instance.Description = ConfigurationManager.AppSettings["Description"];

            if(string.IsNullOrWhiteSpace(_instance.ServiceName))
                throw new ApplicationException("ServiceName is null");

            if (string.IsNullOrWhiteSpace(_instance.DisplayName))
                throw new ApplicationException("DisplayName is null");

            if (string.IsNullOrWhiteSpace(_instance.Description))
                throw new ApplicationException("Description is null");
        }

        private static readonly AppConfig _instance = new AppConfig();

        public static AppConfig Instance {
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
    }
}
