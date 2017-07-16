using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using MailerCommon.Helpers;
using MailerCommon.Interfaces.Services;
using MailerService.Constants;
using MailerService.Helpers;
using MailerService.Interfaces;

namespace MailerService.Infrastructure
{
    public class MailerService : IMailerService
    {
        private readonly IEmailQueueService _emailQueueService;

        //TODO install some IoC container
        public MailerService(IEmailQueueService emailQueueService)
        {
            _emailQueueService = emailQueueService;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Process()
        {
            var emailsQueue = _emailQueueService.GetEmailsToProcess();
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
                    var intervalAfterFailSendingAttemptInSeconds = ConfigurationHelper.GetNumber(ConfigurationNames.IntervalAfterFailSendingAttemptInSeconds,
                        ConfiguratoinDefaultValues.IntervalAfterFailSendingAttemptInSeconds);
                    _emailQueueService.MarkFailure(emailQueue.EmailQueueId, intervalAfterFailSendingAttemptInSeconds);
                }
            }
        }
    }
}
