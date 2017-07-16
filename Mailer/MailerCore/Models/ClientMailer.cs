using System.Runtime.InteropServices.WindowsRuntime;
using MailerCommon.Dto;
using MailerCommon.Interfaces.Services;
using MailerCommon.Models;
using MailerCore.Interfaces;

namespace MailerCore.Models
{
    public class ClientMailer : IClientMailer
    {
        private readonly IEmailQueueService _emailQueueService;

        public ClientMailer(IEmailQueueService emailQueueService)
        {
            _emailQueueService = emailQueueService;
        }

        public ClientMailerSendStatus Send(EmailQueueDto clientMailerSendDto)
        {
            return _emailQueueService.Save(clientMailerSendDto);
        }
    }
}
