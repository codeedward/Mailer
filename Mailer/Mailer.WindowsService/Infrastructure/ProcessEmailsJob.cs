using System;
using System.IO;
using Mailer.Service.Interface.WS.Service;
using Mailer.Utilities.Helpers;
using Quartz;

namespace Mailer.WindowsService.Infrastructure
{
    [DisallowConcurrentExecution]
    public class ProcessEmailsJob : IJob
    {
        private readonly IEmailProcessorService _emailProcessorService;

        public ProcessEmailsJob(IEmailProcessorService emailProcessorService)
        {
            _emailProcessorService = emailProcessorService;
        }

        public void Execute(IJobExecutionContext context)
        {
            LogHelper.Info("Start processing emails.");
            Process();
            LogHelper.Info("Finish processing emails.");
        }

        public void Process()
        {
           _emailProcessorService.Process();
        }

        public void ProcessTest(string stage = "Continue")
        {
            string path = string.Format(@"C:\TopShelfSaveFileTest\{0}_{1}.txt", DateTime.Now.ToLongTimeString().Replace(':', '_'), stage);
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine("The very first line!");
                tw.Close();
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("The next line!");
                    tw.Close();
                }
            }
        }
    }
}
