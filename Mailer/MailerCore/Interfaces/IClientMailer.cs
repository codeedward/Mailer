using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailerCore.Models;

namespace MailerCore.Interfaces
{
    public interface IClientMailer
    {
        ClientMailerSendStatus Send(ClientMailerSendDto clientMailerSendDto);
    }
}
