using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailerCommon.Dto;

namespace MailerService.Helpers
{
    public class EmailProcessorHelper
    {
        public static bool Process(EmailQueueDto emailQueue)
        {
            //TODO rethink it twice how to pass that new values to SendEmail method
            var readySubject = ReplaceReplacements(emailQueue.SubjectTemplate, emailQueue.Replacements);
            var readyBody = ReplaceReplacements(emailQueue.BodyTemplate, emailQueue.Replacements);
            var sendEmailDto = new SendComplexEmailDto(emailQueue.To, emailQueue.Cc, emailQueue.Bcc, emailQueue.From, readyBody, readySubject, emailQueue.Host, emailQueue.Port);
            return EmailHelper.SendEmail(sendEmailDto);
        }

        private static string ReplaceReplacements(string emailQueueSubjectTemplate, List<EmailReplacementDto> emailQueueReplacements)
        {
            var readyText = emailQueueSubjectTemplate;
            foreach (var emailReplacement in emailQueueReplacements)
            {
                readyText = readyText.Replace(emailReplacement.Token, emailReplacement.Value);
            }
            return readyText;
        }
    }
}
