using System.Configuration;

namespace Mailer.Utilities.Helpers
{
    public static class ConfigurationHelper
    {
        public static int GetNumber(string key, int defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            return int.TryParse(value, out var result) ? result : defaultValue;
        }

        public static bool GetBoolean(string key, bool defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            return bool.TryParse(value, out var result) ? result : defaultValue;
        }
    }
}
