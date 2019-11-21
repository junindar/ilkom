using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace LatihanWinServiceAndQuartz
{

  
    public  class JobClass : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Log.WriteLog("Proses Start");
            await Task.Delay(5000);
            Log.WriteLog("Proses Finish");
        }
    }

    public class JobService
    {
        public void OnStart()
        {
            Log.WriteLog("Service Start");
            Task<IScheduler> scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Result.Start();

            IJobDetail job = JobBuilder.Create<JobClass>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("LatihanQuartz", "LatihanQuartz")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();

            scheduler.Result.ScheduleJob(job, trigger);


        }

        public void OnStop()
        {
            Log.WriteLog("Service Stop");
        }

    }

}
