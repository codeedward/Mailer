using System.Collections.Generic;
using Mailer.Domain.Core;

namespace Mailer.Repository.Interface.Core
{
    public interface IEmailQueueRepository
    {
        List<long> Save(CoreEmailDto emailQueue);
    }
}