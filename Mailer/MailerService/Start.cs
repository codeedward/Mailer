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
