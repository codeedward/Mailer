using System;
using System.Collections.Generic;
using Mailer.DAL.Db;
using Mailer.Domain.WS;
using System.Linq;
using Mailer.Common.Enums;
using Mailer.Common.Models;
using Mailer.Repository.Interface.WS;


namespace Mailer.DAL.Repository.WS
{
    public class EmailQueueRepository : RepositoryBase, IEmailQueueRepository
    {
        public List<EmailQueueDto> GetEmailsToProcess()
        {
            using (var dbContext = MailerContext)
            {
                var emailsResult = new List<EmailQueueDto>();

                var queryGetEmailsToProcess =
                    from emailQueue in dbContext.EmailQueues
                    join message in dbContext.EmailMessages on emailQueue.EmailMessageId equals message.EmailMessageId
                    join replacement in dbContext.EmailReplacements on emailQueue.EmailQueueId equals replacement.EmailQueueId
                    into replacementsForEmail
                    where
                    emailQueue.EmailStatus == (byte)EmailQueueStatus.Unprocessed
                    && emailQueue.AvailableToSendFromUtc.Value < DateTime.UtcNow
                    orderby emailQueue.CreatedOn
                    select new
                    {
                        EmailQueue = emailQueue,
                        EmailMessage = message,
                        EmailReplacements = replacementsForEmail
                    };

                var emailsToProcess = queryGetEmailsToProcess.ToList();
                foreach (var email in emailsToProcess)
                {
                    emailsResult.Add(new EmailQueueDto()
                    {
                        EmailQueueId = email.EmailQueue.EmailQueueId,
                        EmailType = email.EmailQueue.EmailType,
                        TriesLeft = email.EmailQueue.TriesLeft,
                        AvailableToSendFromUtc = email.EmailQueue.AvailableToSendFromUtc,
                        From = new EmailRecipient(email.EmailMessage.FromAddress, email.EmailMessage.FromPerson),
                        To = new EmailRecipient(email.EmailQueue.ToEmailAddress, email.EmailQueue.ToPerson),
                        SubjectTemplate = email.EmailMessage.SubjectTemplate,
                        BodyTemplate = email.EmailMessage.BodyTemplate,
                        Host = email.EmailMessage.Host,
                        Port = email.EmailMessage.Port,
                        Replacements = email.EmailReplacements
                            .Select(x => new EmailReplacementDto()
                                {
                                    EmailReplacementId = x.EmailReplacementId,
                                    Value = x.Value,
                                    Token = x.Token
                                }
                            )
                            .ToList(),
                    });
                }
                return emailsResult;
            }
        }

        public bool MarkAsProcessed(long emailQueueId)
        {
            using (var dbContext = MailerContext)
            {
                var emailQueue = new EmailQueue()
                {
                    EmailQueueId = emailQueueId,
                    EmailStatus = (byte)EmailQueueStatus.Processed,
                    SendDateUtc = DateTime.UtcNow,
                    LastTryDateUtc = DateTime.UtcNow,

                };

                dbContext.EmailQueues.Attach(emailQueue);
                dbContext.Entry(emailQueue).Property(x => x.EmailStatus).IsModified = true;
                dbContext.Entry(emailQueue).Property(x => x.SendDateUtc).IsModified = true;
                dbContext.Entry(emailQueue).Property(x => x.LastTryDateUtc).IsModified = true;
                dbContext.Configuration.ValidateOnSaveEnabled = false;

                return dbContext.SaveChanges() > 0;
            }
        }

        public bool MarkFailure(long emailQueueId, long intervalAfterFailSendingAttemptInSeconds)
        {
            using (var dbContext = MailerContext)
            {
                var item = dbContext.EmailQueues.FirstOrDefault(x => x.EmailQueueId == emailQueueId);
                if (item != null)
                {
                    item.LastTryDateUtc = DateTime.UtcNow;
                    item.TriesLeft--;

                    if (item.TriesLeft > 0)
                    {
                        item.AvailableToSendFromUtc = DateTime.UtcNow.AddSeconds(intervalAfterFailSendingAttemptInSeconds);
                    }
                    else
                    {
                        item.EmailStatus = (byte)EmailQueueStatus.Error;
                    }
                }
                return dbContext.SaveChanges() > 0;
            }
        }
    }
}
