using System.Collections.Generic;
using MailerBllDto;
using MailerCommon.Models;

namespace MailerInterface.Services
{
    //TODO GetEmailsToProcess will get everything (replacements & receivers) on one go through entity
    public interface IEmailQueueService
    {
        ClientMailerSendStatus Save(EmailQueueDto emailQueue);
        List<EmailQueueDto> GetEmailsToProcess();
        bool MarkAsProcessed(long emailQueueId);
        bool MarkFailure(long emailQueue, int intervalAfterFailSendingAttemptInSeconds);
    }
}
