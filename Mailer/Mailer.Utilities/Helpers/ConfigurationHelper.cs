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
    }
}
