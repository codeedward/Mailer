using Mailer.WindowsService.Infrastructure;

namespace Mailer.WindowsService
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
