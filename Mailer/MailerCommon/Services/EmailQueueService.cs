using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailerCommon.Dto;
using MailerCommon.Interfaces.Services;

namespace MailerCommon.Services
{
    public class EmailQueueService : IEmailQueueService
    {
        public long Save(EmailQueueDto emailQueue)
        {
            throw new NotImplementedException();
        }

        public List<EmailQueueDto> GetEmailsToProcess()
        {
            //3.Send email through .Net(and save date to variable)
            //a) Get replacements
            //var replacements = _replacementService.GetEmailReplacements(emailQueue.EmailQueueId);
            //b) Get receivers
            //var receivers = _receiverService
            throw new NotImplementedException();
        }

        public bool MarkAsProcessed(long emailQueueId)
        {
            throw new NotImplementedException();
        }

        public bool MarkFailure(long emailQueueEmailQueueId)
        {
            //3.Decrement value of tries, value of AvailableToSendUtc, or change mail status to error.
            throw new NotImplementedException();
        }
    }
}
