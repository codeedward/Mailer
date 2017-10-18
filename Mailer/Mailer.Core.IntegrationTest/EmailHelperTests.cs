using Mailer.Common.Dto;
using Mailer.Common.Models;
using Mailer.Utilities.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mailer.Core.IntegrationTest
{
    [TestClass]
    public class EmailHelperTests
    {
        [TestMethod]
        public void SendEmailTest()
        {
            string senderEmail = "";
            string senderName = "";
            string receiverEmail = "";
            string receiverName = "";
            EmailHelper.SendEmail(
                new SendEmailDto(0, new EmailRecipient(senderEmail, senderName), new EmailRecipient(receiverEmail, receiverName), "body", "subject", "smtp.gmail.com", 587 )
                );
        }
    }
}
