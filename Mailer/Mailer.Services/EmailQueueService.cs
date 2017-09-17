using System;
using System.Collections.Generic;
using Mailer.Common.Enums;
using Mailer.Common.Models;
using Mailer.Domain.Core;
using Mailer.Domain.WS;
using Mailer.Repository.Interface;
using Mailer.Service.Interface.Services;
using Mailer.Utilities.Helpers;

namespace Mailer.Services
{
    public class EmailQueueService : IEmailQueueService
    {
        private readonly IEmailQueueRepository _emailQueueRepository;

        public EmailQueueService(IEmailQueueRepository emailQueueRepository)
        {
            _emailQueueRepository = emailQueueRepository;
        }

        public ClientMailerSendStatus Save(CoreEmailDto emailQueueDto)
        {
            try
            {
                var id = _emailQueueRepository.Save(emailQueueDto);
                return new ClientMailerSendStatus(StatusMailerSend.Ok, id);
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                return new ClientMailerSendStatus(StatusMailerSend.Error, e.Message);
            }
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
