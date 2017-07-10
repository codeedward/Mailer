using System.Collections.Generic;
using MailerCommon.Dto;

namespace MailerCommon.Interfaces.Services
{
    public interface IEmailQueueService
    {
        long Save(EmailQueueDto emailQueue);
        List<EmailQueueDto> GetEmailsToProcess();
        void MarkAsProcessed(long emailQueueId);
    }
}
