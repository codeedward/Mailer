using System;
using System.Collections.Generic;
using MailerBllDto;
using MailerCommon.Enums;
using MailerCommon.Models;
using MailerDto;
using MailerInterface.Repositories;
using MailerInterface.Services;
using MailerUtilities.Helpers;

namespace MailerServices
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
