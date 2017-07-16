using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailerCommon.Dto;
using MailerCommon.Enums;
using MailerCommon.Interfaces.Repositories;
using MailerCommon.Interfaces.Services;
using MailerCommon.Models;

namespace MailerCommon.Services
{
    public class EmailQueueService : IEmailQueueService
    {
        private readonly IEmailQueueRepository _emailQueueRepository;

        public EmailQueueService(IEmailQueueRepository emailQueueRepository)
        {
            _emailQueueRepository = emailQueueRepository;
        }

        public ClientMailerSendStatus Save(EmailQueueDto emailQueueDto)
        {
            try
            {
                var id = _emailQueueRepository.Save(emailQueueDto);
                return new ClientMailerSendStatus(StatusMailerSend.Ok, id);
            }
            catch (Exception e)
            {
                //TODO: Log error
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
                //TODO: Log error
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
                //TODO: Log error
                return false;
            }
        }

        public bool MarkFailure(long emailQueueId, long intervalAfterFailSendingAttemptInSeconds)
        {
            try
            {
                var emailsToProcess = _emailQueueRepository.MarkFailure(emailQueueId, intervalAfterFailSendingAttemptInSeconds);
                return emailsToProcess;
            }
            catch (Exception e)
            {
                //TODO: Log error
                return false;
            }
        }
    }
}
