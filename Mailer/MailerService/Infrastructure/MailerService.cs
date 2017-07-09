using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailerService.Infrastructure
{
    public class MailerService
    {

        //TODO Process mails prototype flow

        /*
         1. Get emails to process (where locked date is null or locked date > now()+some_value && tries>0) and set their LockedDate 
         (some part of emails only, depends on configurable value) - IMailerQueueService
        foreach(emails)
        {
            2. Process one by one 
            //Everything in transaction
            bool sendSuccess = false;
            try
            {
                3. Send email through .Net (and save date to variable)
                sendSuccess = true;
            }
            catch
            {
                3. Decrement value of tries, value of AvailableToSendUtc, or change mail status to error.
                Log error - error during send emails 
            }

            try
            {
                4. Update status to processed and SendDate - IMailerQueueService
            } 
            catch
            {
                4. Log error - error during update email status
            }

        }
         */


    }
}
