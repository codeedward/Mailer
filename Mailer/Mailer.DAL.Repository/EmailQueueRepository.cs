using System;
using System.Collections.Generic;
using System.Transactions;
using Mailer.DAL.Db;
using Mailer.Domain.Core;
using Mailer.Domain.WS;
using System.Linq;
using Mailer.Common.Enums;
using Mailer.Common.Models;
using Mailer.Repository.Interface;

namespace Mailer.DAL.Repository
{
    // TODO test repo
    public class EmailQueueRepository : RepositoryBase, IEmailQueueRepository
    {
        public List<long> Save(CoreEmailDto emailQueueDto)
        {
            using (var dbContext = MailerContext)
            {
                var savedEmailIds = new List<long>();
                using (var trans = new TransactionScope())
                {
                    var emailMessage = new EmailMessage
                    {
                        FromAddress = emailQueueDto.From.EmailAddress,
                        FromPerson = emailQueueDto.From.DisplayName,
                        SubjectTemplate = emailQueueDto.SubjectTemplate,
                        BodyTemplate = emailQueueDto.BodyTemplate,
                        Host = emailQueueDto.Host,
                        Port = emailQueueDto.Port
                    };

                    dbContext.EmailMessages.Add(emailMessage);
                    dbContext.SaveChanges();

                    var newEmailMessageId = emailMessage.EmailMessageId;
                    foreach (var receiverMail in emailQueueDto.To)
                    {
                        var emailQueue = new EmailQueue
                        {
                            EmailMessageId = newEmailMessageId,
                            EmailStatus = (byte)EmailQueueStatus.Unprocessed,
                            EmailType = emailQueueDto.EmailType,
                            TriesLeft = emailQueueDto.TriesLeft,
                            AvailableToSendFromUtc = DateTime.UtcNow,
                            CreatedOn = DateTime.UtcNow,
                            CreatedBy = "System",   // TODO change that
                            ToEmailAddress = receiverMail.EmailAddress,
                            ToPerson = receiverMail.DisplayName
                        };

                        var replacements = new List<EmailReplacement>();
                        foreach (var replacementDto in emailQueue.EmailReplacements)
                        {
                            var replacement = new EmailReplacement
                            {
                                Token = replacementDto.Token,
                                Value = replacementDto.Value
                            };
                            replacements.Add(replacement);
                        }
                        replacements.ForEach(x => emailQueue.EmailReplacements.Add(x));

                        dbContext.EmailQueues.Add(emailQueue);
                        dbContext.SaveChanges();

                        savedEmailIds.Add(emailQueue.EmailQueueId);
                    }

                    trans.Complete();
                }

                return savedEmailIds; 
            }
        }

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
