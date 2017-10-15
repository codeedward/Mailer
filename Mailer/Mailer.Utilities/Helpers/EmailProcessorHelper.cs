using System.Collections.Generic;
using Mailer.Common.Dto;
using Mailer.Domain.WS;


namespace Mailer.Utilities.Helpers
{
    public class EmailProcessorHelper
    {
        public static bool Process(EmailQueueDto emailQueue)
        {
            var readySubject = ReplaceReplacements(emailQueue.SubjectTemplate, emailQueue.Replacements);
            var readyBody = ReplaceReplacements(emailQueue.BodyTemplate, emailQueue.Replacements);
            var sendEmailDto = new SendEmailDto(emailQueue.EmailQueueId, emailQueue.From, emailQueue.To, readyBody, readySubject, emailQueue.Host, emailQueue.Port);

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
