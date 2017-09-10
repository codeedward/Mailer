using MailerCommon.Models;

namespace MailerCommon.Dto
{
    public class SendEmailDto
    {
        public EmailRecipient FromAddress { get; set; }
        public EmailRecipient ToAddress { get; set; }
        public string MessageBody { get; set; }
        public string Subject { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public SendEmailDto(EmailRecipient fromAddress, EmailRecipient toAddress, string messageBody, string subject, string host, int port)
        {
            FromAddress = fromAddress;
            MessageBody = messageBody;
            Subject = subject;
            Host = host;
            Port = port;
            ToAddress = toAddress;
        }
    }
}
