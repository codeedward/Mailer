using System.Collections.Generic;
using Mailer.Domain.Core;
using Mailer.Domain.WS;

namespace Mailer.Repository.Interface
{
    public interface IEmailQueueRepository
    {
        List<long> Save(CoreEmailDto emailQueue);
        List<EmailQueueDto> GetEmailsToProcess();
        bool MarkAsProcessed(long emailQueueId);
        bool MarkFailure(long emailQueueId, long intervalAfterFailSendingAttemptInSeconds);
    }
}