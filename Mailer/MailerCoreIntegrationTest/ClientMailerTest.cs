using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MailerCommon.Models;
using MailerCore.Models;
using MailerDto;
using MailerRepository;
using MailerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailerCoreIntegrationTest
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
