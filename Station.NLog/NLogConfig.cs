using System.IO;
using System.Reflection;
using NLog;
using NLog.Config;
using NLog.Web;

namespace Station.NLog
{
    public static class NLogConfig
    {
        public static LogFactory ConfigureNLog(string configFileName)
        {
            ConfigurationItemFactory.Default.RegisterItemsFromAssembly(typeof(AspNetExtensions).GetTypeInfo().Assembly);
            return LogManager.LoadConfiguration(configFileName);
        }
    }
}