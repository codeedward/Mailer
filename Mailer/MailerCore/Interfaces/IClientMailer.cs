using MailerBllDto;
using MailerCommon.Models;

namespace MailerCore.Interfaces
{
    public interface IClientMailer
    {
        ClientMailerSendStatus Send(EmailQueueDto clientMailerSendDto);
    }
}
