using Mailer.Common.Models;
using Mailer.Core.Interfaces;
using Mailer.Domain.Core;
using Mailer.Service.Interface.Services;

namespace Mailer.Core.Models
{
    public class ClientMailer : IClientMailer
    {
        private readonly IEmailQueueService _emailQueueService;

        public ClientMailer(IEmailQueueService emailQueueService)
        {
            _emailQueueService = emailQueueService;
        }

        public ClientMailerSendStatus Send(CoreEmailDto clientMailerSendDto)
        {
            return _emailQueueService.Save(clientMailerSendDto);
        }
    }
}
