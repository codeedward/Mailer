using System.Collections.Generic;
using Mailer.Domain.WS;

namespace Mailer.Service.Interface.Services
{
    public interface IEmailQueueServiceWs
    {
        List<EmailQueueDto> GetEmailsToProcess();
        bool MarkAsProcessed(long emailQueueId);
        bool MarkFailure(long emailQueue, int intervalAfterFailSendingAttemptInSeconds);
    }
}
