using System.Collections.Generic;
using MailerCommon.Dto;

namespace MailerCommon.Interfaces.Repositories
{
    public interface IEmailQueueRepository
    {
        long Save(EmailQueueDto emailQueue);
        List<EmailQueueDto> GetEmailsToProcess();
        bool MarkAsProcessed(long emailQueueId);
        bool MarkFailure(long emailQueueEmailQueueId);
    }
}