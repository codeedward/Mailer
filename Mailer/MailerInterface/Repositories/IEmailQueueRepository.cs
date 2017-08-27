using System.Collections.Generic;
using MailerBllDto;

namespace MailerInterface.Repositories
{
    public interface IEmailQueueRepository
    {
        long Save(EmailQueueDto emailQueue);
        List<EmailQueueDto> GetEmailsToProcess();
        bool MarkAsProcessed(long emailQueueId);
        bool MarkFailure(long emailQueueEmailQueueId, long intervalAfterFailSendingAttemptInSeconds);
    }
}