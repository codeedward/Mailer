using MailerService.Interfaces;
using Quartz;
using Quartz.Impl;

namespace MailerService.Infrastructure
{
    public class MailerService : IMailerService
    {
        private ISchedulerFactory _schedulerFactory;
        private IScheduler _sched;

        //TODO install some IoC container
        public MailerService()
        {
            //TODO add this to IoC
            InitializeScheduler();
        }

        public void Start()
        {
            _sched.Start();
        }

        public void Stop()
        {
            _sched.PauseAll();
        }

        private void InitializeScheduler()
        {
            _schedulerFactory = new StdSchedulerFactory();

            _sched = _schedulerFactory.GetScheduler();

            IJobDetail job = JobBuilder.Create<ProcessEmailsJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            _sched.ScheduleJob(job, trigger);
        }
    }
}
