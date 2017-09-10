using System.Collections.Generic;
using MailerCommon.Enums;

namespace MailerCommon.Models
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