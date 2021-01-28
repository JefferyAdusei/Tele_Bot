using System;
using System.Threading;

namespace Bot.Core.Schedules
{
    using System.Threading.Tasks;
    using Quartz;
    using Quartz.Impl;

    /// <summary>
    /// Contains methods for beginning the scheduler.
    /// </summary>
    public static class Schedule
    {
        public static void Begin()
        {
            Task<IScheduler> scheduler = new StdSchedulerFactory().GetScheduler();

            // Schedule the birthday job
            scheduler.Result.ScheduleJob(JobBuilder
                                             .Create<BirthdayJob>()
                                             .Build(),
                                         TriggerBuilder
                                             .Create()
                                             .WithCronSchedule("0 00 01 ? * *")
                                             .StartNow()
                                             .Build());
            // TODO: Store CRON schedules in a settings file

            // Schedule the check up job.
            scheduler.Result.ScheduleJob(JobBuilder
                                             .Create<CheckUpJob>()
                                             .Build(),
                                         TriggerBuilder
                                             .Create()
                                             .WithCronSchedule("0 00 06 ? * SUN-SAT")
                                             .StartNow()
                                             .Build());
            scheduler.Result.Start();
        }
    }
}