using System.Collections.Generic;
using MailerCommon.Dto;

namespace MailerCommon.Interfaces.Services
{
    //TODO GetEmailsToProcess will get everything (replacements & receivers) on one go through entity
    public interface IEmailQueueService
    {
        long Save(EmailQueueDto emailQueue);
        List<EmailQueueDto> GetEmailsToProcess();
        bool MarkAsProcessed(long emailQueueId);
        bool MarkFailure(long emailQueueEmailQueueId);
    }
}
