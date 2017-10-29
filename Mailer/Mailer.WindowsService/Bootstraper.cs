using Mailer.DAL.Repository.WS;
using Mailer.Repository.Interface.WS;
using Mailer.Service.Interface.WS.Service;
using Mailer.Service.WS;
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
            container.RegisterType<IEmailProcessorService, EmailProcessorService>(); 
            container.RegisterType<IEmailQueueServiceWs, EmailQueueServiceWs>(); 
            container.RegisterType<IScheduler>(new InjectionFactory(c => new StdSchedulerFactory().GetScheduler()));
        }
    }
}
