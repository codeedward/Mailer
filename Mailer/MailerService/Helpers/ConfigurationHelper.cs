using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailerService.Helpers
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
