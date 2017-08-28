﻿using MailerCommon.Helpers;
using MailerInterface.Services;
using System.Transactions;
using MailerCommon.Constants;

namespace MailerServices
{
    public class EmailProcessorService : IEmailProcessorService
    {
        private readonly IEmailQueueService _emailQueueService;

        public EmailProcessorService(IEmailQueueService emailQueueService)
        {
            _emailQueueService = emailQueueService;
        }

        public void Process()
        {
            var emailsQueue = _emailQueueService.GetEmailsToProcess();
            if (emailsQueue != null)
            {
                foreach (var emailQueue in emailsQueue)
                {
                    var sendSuccess = false;
                    bool markAsProcessed;
                    using (var trans = new TransactionScope())
                    {
                        markAsProcessed = _emailQueueService.MarkAsProcessed(emailQueue.EmailQueueId);
                        if (markAsProcessed)
                        {
                            sendSuccess = EmailProcessorHelper.Process(emailQueue);
                            if (sendSuccess)
                            {
                                trans.Complete();
                            }
                        }
                    }
                    if (!sendSuccess && markAsProcessed)
                    {
                        //TODO check out this configuration values
                        var intervalAfterFailSendingAttemptInSeconds = ConfigurationHelper.GetNumber(ConfigurationNames.IntervalAfterFailSendingAttemptInSeconds,
                            ConfiguratoinDefaultValues.IntervalAfterFailSendingAttemptInSeconds);
                        _emailQueueService.MarkFailure(emailQueue.EmailQueueId, intervalAfterFailSendingAttemptInSeconds);
                    }
                }
            }
        }
    }
}
