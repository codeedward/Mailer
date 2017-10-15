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
            var emailsQueue = _emailQueueService.GetEmailsToProcess();
            if (emailsQueue?.Count > 0)
            {
                foreach (var emailQueue in emailsQueue)
                {
                    LogHelper.Info($"Processing email id: {emailQueue.EmailQueueId}.");
                    var sendSuccess = false;
                    using (var trans = new TransactionScope())
                    {
                        var markAsProcessed = _emailQueueService.MarkAsProcessed(emailQueue.EmailQueueId);
                        if (markAsProcessed)
                        {
                            LogHelper.Info("Trying to send email.");
                            sendSuccess = EmailProcessorHelper.Process(emailQueue);
                            if (sendSuccess)
                            {
                                LogHelper.Info($"Email was sent id: {emailQueue.EmailQueueId}");
                                trans.Complete();
                            }
                        }
                    }
                    if (!sendSuccess)
                    {
                        LogHelper.Error($"Email was NOT sent id: {emailQueue.EmailQueueId}.");
                        var intervalAfterFailSendingAttemptInSeconds = ConfigurationHelper.GetNumber(ConfigurationNames.IntervalAfterFailSendingAttemptInSeconds,
                            ConfiguratoinDefaultValues.IntervalAfterFailSendingAttemptInSeconds);
                        _emailQueueService.MarkFailure(emailQueue.EmailQueueId, intervalAfterFailSendingAttemptInSeconds);
                    }
                }
            }
            else
            {
                LogHelper.Info("No emails to process.");
            }
        }
    }
}
