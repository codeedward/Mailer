using System.Collections.Generic;
using Mailer.Common.Enums;

namespace Mailer.Common.Models
{
    public class ClientMailerSendStatus
    {
        public StatusMailerSend Status { get; set; }
        public string ErrorMessage { get; set; }
        public List<long> MailIds { get; set; }

        public ClientMailerSendStatus(StatusMailerSend status, List<long> mailIds)
        {
            Status = status;
            MailIds = mailIds;
        }

        public ClientMailerSendStatus(StatusMailerSend status, string errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }
    }
}