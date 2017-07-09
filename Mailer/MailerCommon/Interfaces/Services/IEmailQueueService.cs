using System.Collections.Generic;

namespace MailerCommon.Interfaces
{
    public interface IEmailQueueService
    {
        //TODO fill with proper objects
        bool Save(object emailQueue);
        List<object> GetEmailsToProcess();
    }
}
