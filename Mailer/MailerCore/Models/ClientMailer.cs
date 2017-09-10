using MailerCommon.Models;
using MailerCore.Interfaces;
using MailerDto;
using MailerInterface.Services;

namespace MailerCore.Models
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
