﻿using MailerCommon.Constants;
using MailerCommon.Interfaces.Services;
using Microsoft.Practices.Unity;
using Quartz;
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
                    service.ConstructUsing(s => new MailerService());
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
