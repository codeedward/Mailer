using System.Runtime.InteropServices.WindowsRuntime;
using MailerCore.Interfaces;

namespace MailerCore.Models
{
    public class ClientMailer : IClientMailer
    {
        public ClientMailerSendStatus Send(ClientMailerSendDto clientMailerSendDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
