using System.Collections.Generic;
using MailerCore.Enums;

namespace MailerCore.Models
{
    public class ClientMailerSendDto
    {
        public EmailType Type { get; set; }
        public string FromPerson { get; set; }
        public string FromAddress { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public List<Replacement> Replacements { get; set; }
    }
}