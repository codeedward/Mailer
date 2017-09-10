using MailerCommon.Constants;
using MailerInterface.WindowsApplication;
using MailerUtilities.Helpers;
using Quartz;
using Microsoft.Practices.Unity;

namespace MailerService.Infrastructure
{
    public class MailerWindowsService : IMailerService
    {
        private IScheduler _sched;

        public MailerWindowsService()
        {
            InitializeScheduler();
        }

        public void Start()
        {
            _sched.Start();
        }

        public void Stop()
        {
            _sched.Shutdown();
        }

        private void InitializeScheduler()
        {
            _sched =  Bootstraper.Container.Resolve<IScheduler>();
            _sched.JobFactory = new UnityJobFactory(Bootstraper.Container);

            IJobDetail job = JobBuilder.Create<ProcessEmailsJob>()
                .WithIdentity("ProcessEmailsJob", "ProcessEmailGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ProcessEmailsTrigger", "ProcessEmailGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(ConfigurationHelper.GetNumber(ConfigurationNames.ProcessEmailsJobInterval,
                            ConfiguratoinDefaultValues.ProcessEmailsJobInterval))
                    .RepeatForever())
                .Build();

            _sched.ScheduleJob(job, trigger);
        }
    }
}
