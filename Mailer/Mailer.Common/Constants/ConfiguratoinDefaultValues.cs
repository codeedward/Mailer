﻿namespace Mailer.Common.Constants
{
    public class ConfiguratoinDefaultValues
    {
        public const int IntervalAfterFailSendingAttemptInSeconds = 3600;
        public const int ProcessEmailsJobInterval = 60;
        public const bool DebugSendingEmailsOn = true;
        public static bool DebugSendingEmailsResultValue = false;
    }
}