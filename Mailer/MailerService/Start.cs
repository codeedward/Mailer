using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailerService.Infrastructure;

namespace MailerService
{
    internal class Start
    {
        private static void Main(string[] args)
        {
            Bootstraper.Initialise();
            MailerServiceConfiguration.Configure();
        }
    }
}
