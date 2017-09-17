using System.Collections.Generic;
using Mailer.Common.Models;
using Mailer.Domain.Core;
using Mailer.Domain.WS;

namespace Mailer.Service.Interface.Services
{
    public interface IEmailQueueService
    {
        ClientMailerSendStatus Save(CoreEmailDto emailQueue);
        List<EmailQueueDto> GetEmailsToProcess();
        bool MarkAsProcessed(long emailQueueId);
        bool MarkFailure(long emailQueue, int intervalAfterFailSendingAttemptInSeconds);
    }
}
