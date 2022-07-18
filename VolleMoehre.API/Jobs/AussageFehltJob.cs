using EasyCronJob.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VolleMoehre.API.Jobs
{
    public class AussageFehltJob : CronJobService
    {
        public AussageFehltJob(ICronConfiguration<AussageFehltJob> cronConfiguration)
            : base(cronConfiguration.CronExpression, cronConfiguration.TimeZoneInfo, cronConfiguration.CronFormat)
        {
        }

        public async override Task DoWork(CancellationToken cancellationToken)
        {
            int count = 0;
            await SlackHelper.SendDirectMessage("Tim", "Job 'Aussage fehlt' läuft los.").ConfigureAwait(true);
            try
            {
                var store = new VolleMoehre.Adapter.LiteDB.LiteDBStore();
                var auftritte = await store.GetAllAsync<VolleMoehre.Contracts.Model.Auftrittstermin>(a => a.Datum.Date >= DateTime.Now.Date).ConfigureAwait(true);
                var trainings = await store.GetAllAsync<VolleMoehre.Contracts.Model.Trainingstermin>(a => a.Datum.Date >= DateTime.Now.Date).ConfigureAwait(true);
                var alleSpieler = await store.GetAllAsync<VolleMoehre.Contracts.Model.Spieler>(s => s.Aktiv).ConfigureAwait(true);
                foreach (var auftritt in auftritte)
                {
                    foreach (var spieler in alleSpieler)
                    {
                        if (!auftritt.Moderator.Contains(spieler.Id) &&
                            !auftritt.Spieler.Contains(spieler.Id) &&
                            !auftritt.Helfer.Contains(spieler.Id) &&
                            !auftritt.Vorgemerkt.Contains(spieler.Id) &&
                            !auftritt.Abwesend.Contains(spieler.Id))
                        {
                            await SlackHelper.SendDirectMessage(spieler.Name, "Du hast zu dem Auftritt (" + auftritt.Showtyp + ") am " + auftritt.Datum.ToString() + " noch keine Aussage gemacht. Bitte hole das noch nach, vielen Dank! :)\nhttps://intern.vollemoehre.de").ConfigureAwait(true);
                            count++;
                        }
                    }
                }

                foreach (var training in trainings)
                {
                    foreach (var spieler in alleSpieler)
                    {
                        if (!training.Leiter.Contains(spieler.Id) &&
                            !training.Teilnehmer.Contains(spieler.Id) &&
                            !training.Online.Contains(spieler.Id) &&
                            !training.Vorgemerkt.Contains(spieler.Id) &&
                            !training.Abwesend.Contains(spieler.Id))
                        {
                            await SlackHelper.SendDirectMessage(spieler.Name, "Du hast zu dem Training (" + training.FreitextInfo + ") am " + training.Datum.ToString() + " noch keine Aussage gemacht. Bitte hole das noch nach, vielen Dank! :)\nhttps://intern.vollemoehre.de").ConfigureAwait(true);
                            count++;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                await SlackHelper.SendDirectMessage("Tim", ex.Message).ConfigureAwait(true);
            }

            await SlackHelper.SendDirectMessage("Tim", "Job 'Aussage fehlt' hat " + count + " Erinnerung erzeugt.").ConfigureAwait(true);
        }
    }
}
