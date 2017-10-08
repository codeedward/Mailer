using Mailer.Common.Models;
using Mailer.Domain.Core;

namespace Mailer.Service.Interface.Core
{
    public interface IEmailQueueServiceCore
    {
        ClientMailerSendStatus Save(CoreEmailDto emailQueue);
    }
}
