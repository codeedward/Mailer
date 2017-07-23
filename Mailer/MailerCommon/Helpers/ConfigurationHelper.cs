using System.Configuration;

namespace MailerCommon.Helpers
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
