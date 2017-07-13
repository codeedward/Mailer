using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailerCommon.Dto
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
