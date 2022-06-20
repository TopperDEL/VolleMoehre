using EasyCronJob.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VolleMoehre.API.Jobs
{
    public class TrainingsReminderJob : CronJobService
    {
        public TrainingsReminderJob(ICronConfiguration<TrainingsReminderJob> cronConfiguration)
            : base(cronConfiguration.CronExpression, cronConfiguration.TimeZoneInfo, cronConfiguration.CronFormat)
        {
        }

        public async override Task DoWork(CancellationToken cancellationToken)
        {
            var store = new VolleMoehre.Adapter.LiteDB.LiteDBStore();
            var trainings = await store.GetAllAsync<VolleMoehre.Contracts.Model.Trainingstermin>(a => a.Datum >= DateTime.Now).ConfigureAwait(true);

            foreach(var training in trainings)
            {
                if(training.Datum.Date == DateTime.Now.AddDays(1).Date)
                {
                    await SlackHelper.SendMessage("#training", "Morgen steht ein Training an: " + training.FreitextInfo).ConfigureAwait(true);
                }
            }
        }
    }
}
