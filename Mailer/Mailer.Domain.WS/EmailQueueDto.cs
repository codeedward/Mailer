using System;
using System.Collections.Generic;
using Mailer.Common.Models;

namespace Mailer.Domain.WS
{
    public class EmailQueueDto
    {
        public long EmailQueueId { get; set; }
        public int EmailType { get; set; }
        public byte TriesLeft { get; set; }
        public DateTime? AvailableToSendFromUtc { get; set; }
        public EmailRecipient From { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public List<EmailReplacementDto> Replacements { get; set; }
        public EmailRecipient To { get; set; }
    }
}