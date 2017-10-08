using Mailer.Common.Models;
using Mailer.Core.Interfaces;
using Mailer.Domain.Core;
using Mailer.Service.Interface.Core;

namespace Mailer.Core.Models
{
    public class ClientMailer : IClientMailer
    {
        private readonly IEmailQueueServiceCore _emailQueueService;

        public ClientMailer(IEmailQueueServiceCore emailQueueService)
        {
            _emailQueueService = emailQueueService;
        }

        public ClientMailerSendStatus Send(CoreEmailDto clientMailerSendDto)
        {
            return _emailQueueService.Save(clientMailerSendDto);
        }
    }
}
