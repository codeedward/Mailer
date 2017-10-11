using System;

namespace Mailer.Utilities.Helpers
{
    public static class LogHelper
    {
        private static readonly log4net.ILog _logger;
        static LogHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
            _logger = log4net.LogManager.GetLogger(typeof(LogHelper));
        }

        public static void Error(Exception exception)
        {
            // TODO add logger execution
            _logger.Error(exception);
        }

        public static void Info(string message)
        {
            // TODO add logger execution
            _logger.Info(message);
        }
    }
}
