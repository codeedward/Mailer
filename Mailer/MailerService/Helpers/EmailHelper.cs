using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using MailerDto;

namespace MailerService.Helpers
{
    public static class EmailHelper
    {
        public static bool SendEmail(SendSimpleEmailDto sendEmailDto)
        {
            var mailTo = AddMailAddressToCollection(new List<MailAddress>(), sendEmailDto.ToAddress);
            var complexMailDto = new SendComplexEmailDto(mailTo, null, null, sendEmailDto);

            return DoSend(complexMailDto);
        }

        public static bool SendEmail(SendComplexEmailDto sendEmailDto)
        {
            return DoSend(sendEmailDto);
        }

        public static List<MailAddress> AddMailAddressToCollection(List<MailAddress> emailAddresses, string email, string displayName = "")
        {
            try
            {
                emailAddresses.Add(new MailAddress(email, displayName));
            }
            catch (Exception ex)
            {
                //TODO log error
            }
            return emailAddresses;
        }

        #region Private methods

        private static bool DoSend(SendComplexEmailDto complexEmailDto)
        {
            try
            {
                var message = new MailMessage
                {
                    IsBodyHtml = true,
                    From = complexEmailDto.FromAddress,
                    Subject = complexEmailDto.Subject,
                    Body = complexEmailDto.MessageBody
                };

                var emptyAddressList = new List<MailAddress>();
                message.To.ToList().AddRange(complexEmailDto.ToAddress ?? emptyAddressList);
                message.CC.ToList().AddRange(complexEmailDto.ToAddressCc ?? emptyAddressList);
                message.Bcc.ToList().AddRange(complexEmailDto.ToAddressBcc ?? emptyAddressList);

                using (var smtpClient = new SmtpClient(complexEmailDto.Host, complexEmailDto.Port))
                {
                    smtpClient.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                return false;
            }
        }
        #endregion
    }
}
