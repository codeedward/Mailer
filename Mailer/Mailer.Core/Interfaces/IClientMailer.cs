using Mailer.Common.Models;
using Mailer.Domain.Core;

namespace Mailer.Core.Interfaces
{
    public interface IClientMailer
    {
        ClientMailerSendStatus Send(CoreEmailDto clientMailerSendDto);
    }
}
