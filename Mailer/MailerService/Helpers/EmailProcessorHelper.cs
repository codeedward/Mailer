using System.Collections.Generic;
using MailerBllDto;
using MailerDto;

namespace MailerService.Helpers
{
    public class EmailProcessorHelper
    {
        public static bool Process(EmailQueueDto emailQueue)
        {
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
