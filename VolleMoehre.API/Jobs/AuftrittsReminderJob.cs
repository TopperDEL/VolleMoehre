using EasyCronJob.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VolleMoehre.API.Jobs
{
    public class AuftrittsReminderJob : CronJobService
    {
        public AuftrittsReminderJob(ICronConfiguration<AuftrittsReminderJob> cronConfiguration)
            : base(cronConfiguration.CronExpression, cronConfiguration.TimeZoneInfo, cronConfiguration.CronFormat)
        {
        }

        public async override Task DoWork(CancellationToken cancellationToken)
        {
            var store = new VolleMoehre.Adapter.LiteDB.LiteDBStore();
            var auftritte = await store.GetAllAsync<VolleMoehre.Contracts.Model.Auftrittstermin>(a => a.Datum >= DateTime.Now).ConfigureAwait(true);

            foreach(var auftritt in auftritte)
            {
                //7 Tage vorher
                if(auftritt.Datum.Date == DateTime.Now.AddDays(7).Date)
                {
                    foreach (var spieler in auftritt.Moderator)
                    {
                        await SlackHelper.SendDirectMessage(spieler, "In einer Woche steht ein Auftritt an, bei dem du als 'Moderator' eingetragen bist: " + auftritt.Showtyp).ConfigureAwait(true);
                    }
                    foreach (var spieler in auftritt.Spieler)
                    {
                        await SlackHelper.SendDirectMessage(spieler, "In einer Woche steht ein Auftritt an, bei dem du als 'Spieler' eingetragen bist: " + auftritt.Showtyp).ConfigureAwait(true);
                    }
                    foreach (var spieler in auftritt.Helfer)
                    {
                        await SlackHelper.SendDirectMessage(spieler, "In einer Woche steht ein Auftritt an, bei dem du als 'Helfer' eingetragen bist: " + auftritt.Showtyp).ConfigureAwait(true);
                    }
                    foreach (var spieler in auftritt.Vorgemerkt)
                    {
                        await SlackHelper.SendDirectMessage(spieler, "In einer Woche steht ein Auftritt an, bei dem du als 'Vorgemerkt' eingetragen bist: " + auftritt.Showtyp).ConfigureAwait(true);
                    }
                }
            }
        }
    }
}
