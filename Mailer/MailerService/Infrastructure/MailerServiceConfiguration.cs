using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailerCommon.Constants;
using MailerCommon.Repositories;
using MailerCommon.Services;
using Topshelf;

namespace MailerService.Infrastructure
{
    public class MailerServiceConfiguration
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.StartAutomatically();
                configure.Service<MailerService>(service =>
                {
                    //TODO use IoC
                    service.ConstructUsing(s => new MailerService(new EmailQueueService(new EmailQueueRepository())));
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });

                configure.RunAsLocalSystem();
                configure.SetServiceName(ServiceNames.MailerService);
                configure.SetDisplayName(ServiceNames.MailerService);
                configure.SetDescription(ServiceNames.MailerService);
            });
        }
    }
}
