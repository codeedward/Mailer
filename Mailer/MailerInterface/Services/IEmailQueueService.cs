using System.Collections.Generic;
using MailerBllDto;
using MailerCommon.Models;
using MailerDto;

namespace MailerInterface.Services
{
    public interface IEmailQueueService
    {
        ClientMailerSendStatus Save(CoreEmailDto emailQueue);
        List<EmailQueueDto> GetEmailsToProcess();
        bool MarkAsProcessed(long emailQueueId);
        bool MarkFailure(long emailQueue, int intervalAfterFailSendingAttemptInSeconds);
    }
}
