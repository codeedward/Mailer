using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailerService.Constants
{
    public class ConfiguratoinDefaultValues
    {
        public const int IntervalAfterFailSendingAttemptInSeconds = 3600;
        public const int ProcessEmailsJobInterval = 60;
    }
}