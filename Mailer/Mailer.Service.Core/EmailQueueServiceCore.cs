using System;
using Mailer.Common.Enums;
using Mailer.Common.Models;
using Mailer.Domain.Core;
using Mailer.Repository.Interface;
using Mailer.Service.Interface.Core;
using Mailer.Utilities.Helpers;

namespace Mailer.Service.Core
{
    public class EmailQueueServiceCore : IEmailQueueServiceCore
    {
        private readonly IEmailQueueRepository _emailQueueRepository;

        public EmailQueueServiceCore(IEmailQueueRepository emailQueueRepository)
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
    }
}
