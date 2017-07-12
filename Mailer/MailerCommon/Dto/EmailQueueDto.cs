using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace MailerCommon.Dto
{
    public class EmailQueueDto
    {
        public long EmailQueueId { get; set; }
        public int EmailType { get; set; }
        public int TriesLeft { get; set; }
        public DateTime AvailableToSendFromUtc { get; set; }
        public MailAddress From { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public List<EmailReplacementDto> Replacements { get; set; }
        public List<MailAddress> To { get; set; }
        public List<MailAddress> Cc { get; set; }
        public List<MailAddress> Bcc { get; set; }
    }
}