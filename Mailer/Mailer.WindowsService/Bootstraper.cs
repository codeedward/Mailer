using Mailer.DAL.Repository;
using Mailer.Repository.Interface;
using Microsoft.Practices.Unity;
using Quartz;
using Quartz.Impl;

namespace Mailer.WindowsService
{
    public static class Bootstraper
    {
        public static UnityContainer Container { get; set; }
        public static void Initialise()
        {
            Container = new UnityContainer();
            RegisterTypes(Container);
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(
                AllClasses.FromAssembliesInBasePath(),
                WithMappings.FromMatchingInterface,
                WithName.Default);

            container.RegisterType<IEmailQueueRepository, EmailQueueRepository>(); 
            container.RegisterType<IScheduler>(new InjectionFactory(c => new StdSchedulerFactory().GetScheduler()));
        }
    }
}
