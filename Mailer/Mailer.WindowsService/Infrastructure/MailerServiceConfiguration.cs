using Mailer.Common.Constants;
using Topshelf;

namespace Mailer.WindowsService.Infrastructure
{
    public class MailerServiceConfiguration
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.StartAutomatically();
                configure.Service<MailerWindowsService>(service =>
                {
                    service.ConstructUsing(s => new MailerWindowsService());
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
