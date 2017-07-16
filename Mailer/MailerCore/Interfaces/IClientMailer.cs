using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailerCommon.Dto;
using MailerCommon.Models;
using MailerCore.Models;

namespace MailerCore.Interfaces
{
    public interface IClientMailer
    {
        ClientMailerSendStatus Send(EmailQueueDto clientMailerSendDto);
    }
}
