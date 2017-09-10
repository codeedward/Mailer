using MailerCommon.Models;
using MailerDto;

namespace MailerCore.Interfaces
{
    public interface IClientMailer
    {
        ClientMailerSendStatus Send(CoreEmailDto clientMailerSendDto);
    }
}
