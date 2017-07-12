using System;
using System.Net.Mail;
using MailerCommon.Dto;

namespace MailerService.Helpers
{
    public static class EmailHelper
    {
        //TODO change that and put it asynchronous call
        public static bool SendEmail(EmailQueueDto emailQueueDto)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.Host = "smtp.internal.mycompany.com";
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("user", "Password");
            mailMessage.From = new MailAddress("from@server.com");
            mailMessage.To.Add(new MailAddress("to@server.com"));
            mailMessage.Subject = "Password Recover";
            mailMessage.Body = "Message";
            client.Send(mailMessage);


            throw new NotImplementedException();
        }
    }
}
