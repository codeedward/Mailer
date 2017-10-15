using System;
using System.Collections.Generic;
using System.Linq;
using Mailer.Common.Models;
using Mailer.Core.Models;
using Mailer.DAL.Repository;
using Mailer.Domain.Core;
using Mailer.Service.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mailer.Core.IntegrationTest
{
    [TestClass]
    public class ClientMailerTest
    {
        [TestMethod]
        public void SaveEmailTest()
        {
            var clientMailer = new ClientMailer(
                new EmailQueueServiceCore(
                    new EmailQueueRepository()
                )
            );

            var response = clientMailer.Send(
                new CoreEmailDto()
                {
                    EmailType = 2,
                    TriesLeft = 5,
                    AvailableToSendFromUtc = DateTime.UtcNow,
                    From = new EmailRecipient("abc@email.com", "abcTestName"),
                    SubjectTemplate = "Subject",
                    BodyTemplate = "Body",
                    Host = "123",
                    Port = 25,
                    To = new List<EmailRecipient>() { new EmailRecipient("abc@email.com", "abcTestName") }
                });

            Assert.IsTrue(response.MailIds.Count == 1);
        }

        [TestMethod]
        public void SaveEmailAndRemoveTest()
        {
            var clientMailer = new ClientMailer(
                new EmailQueueServiceCore(
                    new EmailQueueRepository()
                )
            );
            
            var response = clientMailer.Send(
                new CoreEmailDto()
                {
                    EmailType = 2,
                    TriesLeft = 5,
                    AvailableToSendFromUtc = DateTime.UtcNow,
                    From = new EmailRecipient("abc@email.com", "abcTestName"),
                    SubjectTemplate = "Subject",
                    BodyTemplate = "Body",
                    Host = "123",
                    Port = 25,
                    To = new List<EmailRecipient>() {new EmailRecipient("abc@email.com", "abcTestName")}
                });

            Assert.IsTrue(response.MailIds.Count == 1);

            using (var dbContext = new EmailQueueRepository().MailerContext)
            {
                var savedEmailId = response.MailIds[0];
                var addedEmailQueue = dbContext.EmailQueues.Where(x => x.EmailQueueId == savedEmailId).ToList().FirstOrDefault();
                var addedEmailMessage = dbContext.EmailMessages.Where(x => x.EmailMessageId == addedEmailQueue.EmailMessageId).ToList().FirstOrDefault();

                Assert.IsNotNull(addedEmailQueue);
                Assert.IsNotNull(addedEmailMessage);

                dbContext.EmailQueues.Remove(addedEmailQueue);
                dbContext.EmailMessages.Remove(addedEmailMessage);
                dbContext.SaveChanges();

                var removedEmailQueue = dbContext.EmailQueues.Where(x => x.EmailQueueId == savedEmailId).ToList().FirstOrDefault();
                var removedEmailMessage = dbContext.EmailMessages.Where(x => x.EmailMessageId == addedEmailQueue.EmailMessageId).ToList().FirstOrDefault();

                Assert.IsNull(removedEmailQueue);
                Assert.IsNull(removedEmailMessage);
            }
        }
    }
}
