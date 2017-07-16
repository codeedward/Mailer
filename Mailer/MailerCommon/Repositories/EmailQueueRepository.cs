using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailerCommon.Dto;
using MailerCommon.Interfaces.Repositories;
using MailerCommon.Models;

namespace MailerCommon.Repositories
{
    public class EmailQueueRepository : IEmailQueueRepository
    {
        public long Save(EmailQueueDto emailQueue)
        {
            //TODO implement Save all this things to db
            //save to db email
            //replacements
            //receivers
            throw new NotImplementedException();
        }

        public List<EmailQueueDto> GetEmailsToProcess()
        {
            //TODO implement
            throw new NotImplementedException();
            //Get emails:
            //- tires left
            //- available to send from utc
            //- email status
            //- locked date(and some timeout)
        }

        public bool MarkAsProcessed(long emailQueueId)
        {
            //TODO implement
            //Save success:
            //- send date
            //- email status
            throw new NotImplementedException();
        }

        public bool MarkFailure(long emailQueueEmailQueueId, long intervalAfterFailSendingAttemptInSeconds)
        {
            //TODO implement
            //3.Decrement value of tries, value of AvailableToSendUtc, or change mail status to error.
            //save fail:
            //-tries left(email status when <= 0)
            //-last try date utc
            //-available to send from utc
            throw new NotImplementedException();
        }
    }
}
