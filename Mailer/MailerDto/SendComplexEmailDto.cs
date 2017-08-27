using System.Collections.Generic;
using System.Net.Mail;

namespace MailerDto
{
    public class SendComplexEmailDto : SendEmailBaseDto
    {
        public List<MailAddress> ToAddress { get; set; }
        public List<MailAddress> ToAddressCc { get; set; }
        public List<MailAddress> ToAddressBcc { get; set; }

        public SendComplexEmailDto(List<MailAddress> toAddress, List<MailAddress> toAddressCc, List<MailAddress> toAddressBcc,
            MailAddress fromAddress, string messageBody, string subject, string host, int port) 
            : base(fromAddress, messageBody, subject, host, port)
        {
            ToAddress = toAddress;
            ToAddressCc = toAddressCc;
            ToAddressBcc = toAddressBcc;
        }

        public SendComplexEmailDto(List<MailAddress> toAddress, List<MailAddress> toAddressCc, List<MailAddress> toAddressBcc, SendSimpleEmailDto simpleEmailDto)
            : base(simpleEmailDto.FromAddress, simpleEmailDto.MessageBody, simpleEmailDto.Subject, simpleEmailDto.Host, simpleEmailDto.Port)
        {
            ToAddress = toAddress;
            ToAddressCc = toAddressCc;
            ToAddressBcc = toAddressBcc;
        }
    }
}
