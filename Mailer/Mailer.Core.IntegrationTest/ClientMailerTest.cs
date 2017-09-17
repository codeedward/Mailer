using System;
using System.Collections.Generic;
using Mailer.Common.Models;
using Mailer.Core.Models;
using Mailer.DAL.Repository;
using Mailer.Domain.Core;
using Mailer.Services;
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
                new EmailQueueService(
                    new EmailQueueRepository()
                )
            );
            
            clientMailer.Send(
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
        }
    }
}
