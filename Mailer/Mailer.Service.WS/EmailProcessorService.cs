using System.Transactions;
using Mailer.Common.Constants;
using Mailer.Service.Interface.WS.Service;
using Mailer.Utilities.Helpers;

namespace Mailer.Service.WS
{
    public class EmailProcessorService : IEmailProcessorService
    {
        private readonly IEmailQueueServiceWs _emailQueueService;

        public EmailProcessorService(IEmailQueueServiceWs emailQueueService)
        {
            _emailQueueService = emailQueueService;
        }

        public void Process()
        {
            LogHelper.Info("Start processing emails");
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
