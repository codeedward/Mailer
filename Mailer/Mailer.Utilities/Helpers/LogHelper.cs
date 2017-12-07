using System;
using System.IO;

namespace Mailer.Utilities.Helpers
{
    public static class LogHelper
    {
        private static readonly log4net.ILog _logger;
        static LogHelper()
        {
            string configurationFilePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Logger.config";
            var loggerFile = new FileInfo(configurationFilePath);

            log4net.Config.XmlConfigurator.Configure(loggerFile);
            _logger = log4net.LogManager.GetLogger(typeof(LogHelper));

            _logger.Info($@"Logger config file path: {configurationFilePath}");
        }

        public static void Error(Exception exception)
        {
            _logger.Error(exception);
        }

        public static void Error(string message)
        {
            _logger.Error(message);
        }

        public static void Info(string message)
        {
            _logger.Info(message);
        }

        public static void Debug(string message)
        {
            _logger.Debug(message);
        }
    }
}
