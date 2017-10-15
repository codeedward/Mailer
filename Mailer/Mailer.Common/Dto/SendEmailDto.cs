using Mailer.Common.Models;

namespace Mailer.Common.Dto
{
    public class SendEmailDto
    {
        public long Id { get; set; }
        public EmailRecipient FromAddress { get; set; }
        public EmailRecipient ToAddress { get; set; }
        public string MessageBody { get; set; }
        public string Subject { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public SendEmailDto(long id, EmailRecipient fromAddress, EmailRecipient toAddress, string messageBody, string subject, string host, int port)
        {
            Id = id;
            FromAddress = fromAddress;
            MessageBody = messageBody;
            Subject = subject;
            Host = host;
            Port = port;
            ToAddress = toAddress;
        }
    }
}
