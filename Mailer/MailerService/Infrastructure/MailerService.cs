using MailerCommon.Helpers;
using MailerService.Constants;
using MailerService.Interfaces;
using Quartz;
using Quartz.Impl;
using Microsoft.Practices.Unity;
using Quartz.Core;

namespace MailerService.Infrastructure
{
    public class MailerService : IMailerService
    {
        private IScheduler _sched;

        public MailerService()
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
                .WithIdentity("myJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
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
