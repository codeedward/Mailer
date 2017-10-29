using System;
using System.Collections.Generic;
using Mailer.Domain.WS;
using Mailer.Repository.Interface.WS;
using Mailer.Service.Interface.WS.Service;
using Mailer.Utilities.Helpers;

namespace Mailer.Service.WS
{
    public class EmailQueueServiceWs : IEmailQueueServiceWs
    {
        private readonly IEmailQueueRepository _emailQueueRepository;

        public EmailQueueServiceWs(IEmailQueueRepository emailQueueRepository)
        {
            _emailQueueRepository = emailQueueRepository;
        }

        public List<EmailQueueDto> GetEmailsToProcess()
        {
            try
            {
                var emailsToProcess = _emailQueueRepository.GetEmailsToProcess();
                return emailsToProcess;
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                return null;
            }
        }

        public bool MarkAsProcessed(long emailQueueId)
        {
            try
            {
                var emailsToProcess = _emailQueueRepository.MarkAsProcessed(emailQueueId);
                return emailsToProcess;
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                return false;
            }
        }

        public bool MarkFailure(long emailQueueId, int intervalAfterFailSendingAttemptInSeconds)
        {
            try
            {
                var emailsToProcess = _emailQueueRepository.MarkFailure(emailQueueId, intervalAfterFailSendingAttemptInSeconds);
                return emailsToProcess;
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                return false;
            }
        }
    }
}
