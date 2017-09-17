﻿using System.Transactions;
using Mailer.Common.Constants;
using Mailer.Service.Interface.Services;
using Mailer.Utilities.Helpers;

namespace Mailer.Services
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
                    using (var trans = new TransactionScope())
                    {
                        var markAsProcessed = _emailQueueService.MarkAsProcessed(emailQueue.EmailQueueId);
                        if (markAsProcessed)
                        {
                            sendSuccess = EmailProcessorHelper.Process(emailQueue);
                            if (sendSuccess)
                            {
                                trans.Complete();
                            }
                        }
                    }
                    if (!sendSuccess)
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