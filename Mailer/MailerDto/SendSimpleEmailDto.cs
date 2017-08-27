using System.Net.Mail;

namespace MailerDto
{
    public class SendSimpleEmailDto : SendEmailBaseDto
    {
        public string ToAddress { get; set; }

        public SendSimpleEmailDto(MailAddress fromAddress, string messageBody, string subject, string host, int port, string toAddress) 
            : base(fromAddress, messageBody, subject, host, port)
        {
            ToAddress = toAddress;
        }
    }
}
