using System;
using System.Net.Mail;
using Mailer.Common.Dto;

namespace Mailer.Utilities.Helpers
{
    public static class EmailHelper
    {
        public static bool SendEmail(SendEmailDto sendEmailDto)
        {
            return DoSend(sendEmailDto);
        }

        #region Private methods
        private static bool DoSend(SendEmailDto sendEmailDto)
        {
            try
            {
                var message = new MailMessage
                {
                    IsBodyHtml = true,
                    From = new MailAddress(sendEmailDto.FromAddress.EmailAddress, sendEmailDto.FromAddress.DisplayName),
                    Subject = sendEmailDto.Subject,
                    Body = sendEmailDto.MessageBody
                };

                using (var smtpClient = new SmtpClient(sendEmailDto.Host, sendEmailDto.Port))
                {
                    message.To.Add(new MailAddress(sendEmailDto.ToAddress.EmailAddress, sendEmailDto.ToAddress.DisplayName));
                    smtpClient.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        
        }
        #endregion 
    }
}
