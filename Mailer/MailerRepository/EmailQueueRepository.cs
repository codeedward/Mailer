using System;
using System.Collections.Generic;
using System.Net.Mail;
using MailerBllDto;
using MailerInterface.Repositories;

namespace MailerRepository
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
            var emails = new List<EmailQueueDto>();
            emails.Add(new EmailQueueDto()
            {
                EmailQueueId = 1,
                EmailType = 1,
                TriesLeft = 5,
                AvailableToSendFromUtc = DateTime.Now,
                From = new MailAddress("test@test.com"),
                SubjectTemplate = "sth",
                BodyTemplate = "sth",
                Host = "123",
                Port = 21,
                Replacements = new List<EmailReplacementDto>(){new EmailReplacementDto(){ EmailReplacementId = 1, Value = "{TEST}", Token = "SOME_VALUE"}},
                To = new List<MailAddress>() { new MailAddress("test@test.com")}
            });
            return emails;

            //TODO implement
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
            //throw new NotImplementedException();
            return false;
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
