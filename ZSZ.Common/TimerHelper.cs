
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;

namespace ZSZ.Common
{
    internal class IntervalJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            object data = context.JobDetail.JobDataMap.Get("dosth") as Action;
            if (data != null && data is Action)
            {
                Action action = data as Action;
                action();
            }
        }
    }
    public class TimerHelper
    {
        public static void ExecuteWithInterval(int second, Action action)
        {
            //scheduler
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            //more job
            {   //job
                JobDetailImpl job = new JobDetailImpl("zszTimer", typeof(IntervalJob));
                job.JobDataMap.Add("dosth", action);//任务数据

                //trigger
                IMutableTrigger trigger =
                    CalendarIntervalScheduleBuilder.Create()
                        .WithIntervalInSeconds(second).Build();
                trigger.Key = new TriggerKey("zszTrigger");

                //watch
                scheduler.ScheduleJob(job, trigger);
            }

            //start            
            scheduler.Start();
        }
    }
}
