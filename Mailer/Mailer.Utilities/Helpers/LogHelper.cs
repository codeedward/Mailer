using System;
using System.IO;

namespace Mailer.Utilities.Helpers
{
    public static class LogHelper
    {
        private static readonly log4net.ILog _logger;
        static LogHelper()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Logger.config"));
            _logger = log4net.LogManager.GetLogger(typeof(LogHelper));
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
    }
}
