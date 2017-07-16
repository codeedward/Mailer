using System.Configuration;

namespace MailerCommon.Helpers
{
    public static class ConfigurationHelper
    {
        public static long GetNumber(string key, long defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            return long.TryParse(value, out var result) ? result : defaultValue;
        }
    }
}
