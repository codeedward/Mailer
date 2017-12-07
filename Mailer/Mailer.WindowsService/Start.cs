using Mailer.Utilities.Helpers;
using Mailer.WindowsService.Infrastructure;

namespace Mailer.WindowsService
{
    internal class Start
    {
        private static void Main(string[] args)
        {
            LogHelper.Debug("Application start");

            try
            {
                Bootstraper.Initialise();
                MailerServiceConfiguration.Configure();
            }
            catch (System.Exception exception)
            {
                LogHelper.Error(exception);
            }
        }
    }
}
