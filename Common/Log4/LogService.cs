using System;
using System.IO;
using System.Reflection;

namespace Common.Log4
{
    public static class LogService
    {
        public static ILogService ILogger { get; private set; }

        static LogService()
        {
            try
            {
                string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log4net.config";
                FileInfo fi = new FileInfo(path);
                log4net.Config.XmlConfigurator.Configure(fi);
                ILogger = new Log(log4net.LogManager.GetLogger("HSUC_Tran_Logger"));
            }
            catch
            {

            }
        }

        public static void ReConfigure(string configFile = "log4net.config")
        {
            if (string.IsNullOrEmpty(configFile)) return;

            try
            {
                var callingAssembly = Assembly.GetCallingAssembly();
                var stream = callingAssembly.GetManifestResourceStream(string.Format("{0}.{1}", callingAssembly.GetName().Name, configFile));
                log4net.Config.XmlConfigurator.Configure(stream);
                ILogger = new Log(log4net.LogManager.GetLogger("HSUC_Tran_Logger"));
            }
            catch
            {

            }
        }

        public static ILogService GetLogger(string loggerName)
        {
            return new Log(log4net.LogManager.GetLogger(loggerName));
        }

        public static ILogService GetLogger(Type type)
        {
            return new Log(log4net.LogManager.GetLogger(type));
        }
    }
}
