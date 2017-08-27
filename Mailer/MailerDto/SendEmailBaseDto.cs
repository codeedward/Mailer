using System.Net.Mail;

namespace MailerDto
{
    public abstract class SendEmailBaseDto
    {
        public MailAddress FromAddress { get; set; }
        public string MessageBody { get; set; }
        public string Subject { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        protected SendEmailBaseDto(MailAddress fromAddress, string messageBody, string subject, string host, int port)
        {
            FromAddress = fromAddress;
            MessageBody = messageBody;
            Subject = subject;
            Host = host;
            Port = port;
        }
    }
}
