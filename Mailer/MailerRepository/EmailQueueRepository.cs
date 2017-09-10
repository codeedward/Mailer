using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using MailerBllDto;
using MailerCommon.Models;
using MailerDb;
using MailerDto;
using MailerInterface.Repositories;

namespace MailerRepository
{
    // TODO test repo
    public class EmailQueueRepository : RepositoryBase, IEmailQueueRepository
    {
        public List<long> Save(CoreEmailDto emailQueueDto)
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

                MailerContext.EmailMessages.Add(emailMessage);
                MailerContext.SaveChanges();

                var newEmailMessageId = emailMessage.EmailMessageId;
                foreach (var receiverMail in emailQueueDto.To)
                {
                    var emailQueue = new EmailQueue
                    {
                        EmailMessageId = newEmailMessageId,
                        EmailStatus = 1,  // TODO status ready to process
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
                    replacements.ForEach(x=> emailQueue.EmailReplacements.Add(x));

                    MailerContext.EmailQueues.Add(emailQueue);
                    MailerContext.SaveChanges();

                    savedEmailIds.Add(emailQueue.EmailQueueId);
                }
                
                trans.Complete();
            }

            return savedEmailIds;
        }

        public List<EmailQueueDto> GetEmailsToProcess()
        {
            var emailsResult = new List<EmailQueueDto>();

            // TODO change here to status enum
            var queryGetEmailsToProcess =
                from emailQueue in MailerContext.EmailQueues
                join message in MailerContext.EmailMessages on emailQueue.EmailMessageId equals message.EmailMessageId
                join replacement in MailerContext.EmailReplacements on emailQueue.EmailQueueId equals replacement.EmailQueueId
                    into replacementsForEmail
                where
                emailQueue.EmailStatus == 2
                && emailQueue.AvailableToSendFromUtc.Value < DateTime.UtcNow.Date
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

        public bool MarkAsProcessed(long emailQueueId)
        {
            var emailQueue = new EmailQueue()
            {
                EmailQueueId = emailQueueId,
                EmailStatus = 2, //todo processed status
                SendDateUtc = DateTime.UtcNow,
                LastTryDateUtc = DateTime.UtcNow,

            };
            MailerContext.Entry(emailQueue).Property(x => x.EmailStatus).IsModified = true;
            MailerContext.Entry(emailQueue).Property(x => x.SendDateUtc).IsModified = true;
            MailerContext.Configuration.ValidateOnSaveEnabled = false;

            return MailerContext.SaveChanges() > 0;
        }

        public bool MarkFailure(long emailQueueId, long intervalAfterFailSendingAttemptInSeconds)
        {
            var item = MailerContext.EmailQueues.FirstOrDefault(x => x.EmailQueueId == emailQueueId);
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
                    item.EmailStatus = 3; //TODO error status
                }
            }
            return MailerContext.SaveChanges() > 0;
        }
    }
}
