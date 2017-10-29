using System.Collections.Generic;
using Mailer.Domain.WS;

namespace Mailer.Repository.Interface.WS
{
    public interface IEmailQueueRepository
    {
        List<EmailQueueDto> GetEmailsToProcess();
        bool MarkAsProcessed(long emailQueueId);
        bool MarkFailure(long emailQueueId, long intervalAfterFailSendingAttemptInSeconds);
    }
}