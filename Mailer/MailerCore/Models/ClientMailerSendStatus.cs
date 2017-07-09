using MailerCore.Enums;

namespace MailerCore.Models
{
    public class ClientMailerSendStatus
    {
        public StatusMailerSend Status { get; set; }
        public string ErrorMessage { get; set; }
        public long MailId { get; set; }

        public ClientMailerSendStatus(StatusMailerSend status, long mailId)
        {
            Status = status;
            MailId = mailId;
        }

        public ClientMailerSendStatus(StatusMailerSend status, string errorMessage, long mailId)
        {
            Status = status;
            ErrorMessage = errorMessage;
            MailId = mailId;
        }
    }
}