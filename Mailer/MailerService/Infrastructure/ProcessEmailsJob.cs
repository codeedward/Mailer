using System;
using System.IO;
using System.Transactions;
using MailerCommon.Helpers;
using MailerCommon.Interfaces.Services;
using MailerService.Constants;
using MailerService.Helpers;
using Microsoft.Practices.Unity;
using Quartz;

namespace MailerService.Infrastructure
{
    public class ProcessEmailsJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Process();
        }

        public void Process()
        {
            var emailQueueService = Bootstraper.Container.Resolve<IEmailQueueService>();
            var emailsQueue = emailQueueService.GetEmailsToProcess();
            if (emailsQueue != null)
            {
                foreach (var emailQueue in emailsQueue)
                {
                    var sendSuccess = false;
                    bool markAsProcessed;
                    using (var trans = new TransactionScope())
                    {
                        markAsProcessed = emailQueueService.MarkAsProcessed(emailQueue.EmailQueueId);
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
                        emailQueueService.MarkFailure(emailQueue.EmailQueueId, intervalAfterFailSendingAttemptInSeconds);
                    }
                } 
            }
        }
        public void ProcessTest(string stage = "Continue")
        {
            string path = string.Format(@"C:\TopShelfSaveFileTest\{0}_{1}.txt", DateTime.Now.ToLongTimeString().Replace(':', '_'), stage);
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine("The very first line!");
                tw.Close();
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("The next line!");
                    tw.Close();
                }
            }
        }
    }
}
