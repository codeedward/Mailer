using System;
using System.Collections.Generic;
using System.Transactions;
using Mailer.Common.Enums;
using Mailer.DAL.Db;
using Mailer.Domain.Core;
using Mailer.Repository.Interface.Core;

namespace Mailer.DAL.Repository.Core
{
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
    }
}
