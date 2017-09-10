using System.Collections.Generic;
using MailerBllDto;
using MailerDto;

namespace MailerInterface.Repositories
{
    public interface IEmailQueueRepository
    {
        List<long> Save(CoreEmailDto emailQueue);
        List<EmailQueueDto> GetEmailsToProcess();
        bool MarkAsProcessed(long emailQueueId);
        bool MarkFailure(long emailQueueId, long intervalAfterFailSendingAttemptInSeconds);
    }
}