using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using MailerCommon.Interfaces;
using MailerCommon.Interfaces.Services;
using MailerService.Helpers;
using MailerService.Interfaces;

namespace MailerService.Infrastructure
{
    public class MailerService : IMailerService
    {
        private readonly IEmailQueueService _emailQueueService;
        private readonly IEmailReplacementService _replacementService;
        private readonly IEmailReceiverService _receiverService;

        //TODO install some IoC container
        public MailerService(
            IEmailQueueService emailQueueService, 
            IEmailReplacementService replacementService,
            IEmailReceiverService receiverService)
        {
            _emailQueueService = emailQueueService;
            _replacementService = replacementService;
            _receiverService = receiverService;
        }

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

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        //TODO sth is wrong with that function - move try/catch inside service methods and use return values to decide if transaction should commit and what to log
        public void Process()
        {
            var emailsQueue = _emailQueueService.GetEmailsToProcess();
            foreach (var emailQueue in emailsQueue)
            {
                //2.Process one by one
                using (TransactionScope trans = new TransactionScope())
                {
                    bool sendSuccess = false;
                    //try
                    //{
                        //TODO description ignore for now, emailQueue should have all that info
                        //3.Send email through .Net(and save date to variable)
                        //a) Get replacements
                        //var replacements = _replacementService.GetEmailReplacements(emailQueue.EmailQueueId);
                        //b) Get receivers
                        //var receivers = _receiverService
                        //c) Send email
                        sendSuccess = EmailHelper.SendEmail(emailQueue);
                    //}
                    //catch (Exception ex)
                    //{
                        //3.Decrement value of tries, value of AvailableToSendUtc, or change mail status to error.
                        //Log error -error during send emails
                    //}

                    //try
                    //{
                       //4.Update status to processed and SendDate - IMailerQueueService
                       _emailQueueService.MarkAsProcessed(emailQueue.EmailQueueId);
                    //}
                    //catch
                    //{
                        //4.Log error - error during update email status
                    //}
                }
            }
            throw new NotImplementedException();
        }
    }
}
