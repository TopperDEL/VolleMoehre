using EasyCronJob.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VolleMoehre.API.Jobs
{
    public class TimTest : CronJobService
    {
        public TimTest(ICronConfiguration<TimTest> cronConfiguration)
            : base(cronConfiguration.CronExpression, cronConfiguration.TimeZoneInfo, cronConfiguration.CronFormat)
        {
        }

        //public async override Task StartAsync(CancellationToken cancellationToken)
        //{
        //    await SlackHelper.SendDirectMessage("Tim", "Test StartAsync - " + DateTime.Now.ToString()).ConfigureAwait(true);
        //    await base.StartAsync(cancellationToken).ConfigureAwait(true);
        //}

        //public async override Task StopAsync(CancellationToken cancellationToken)
        //{
        //    await SlackHelper.SendDirectMessage("Tim", "Test StopAsync - " + DateTime.Now.ToString()).ConfigureAwait(true);
        //    await base.StopAsync(cancellationToken).ConfigureAwait(true);
        //}

        public async override Task DoWork(CancellationToken cancellationToken)
        {
            //SlackHelper.SendDirectMessage("Tim", "Test direct - " + DateTime.Now.ToString());
            await SlackHelper.SendDirectMessage("Tim", "Test - " + DateTime.Now.ToString()).ConfigureAwait(true);
            //await base.DoWork(cancellationToken).ConfigureAwait(true);
        }
        //protected async override Task ScheduleJob(CancellationToken cancellationToken)
        //{
        //    await SlackHelper.SendDirectMessage("Tim", "Tasks scheduled!").ConfigureAwait(true);
        //    //await SlackHelper.SendDirectMessage("Goeksen", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
        //    //await SlackHelper.SendDirectMessage("Michael", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
        //    //await SlackHelper.SendDirectMessage("PhilippW", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
        //    //await SlackHelper.SendDirectMessage("PhilippH", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
        //    //await SlackHelper.SendDirectMessage("Lisa", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
        //    //await SlackHelper.SendDirectMessage("Kalli", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
        //    //await SlackHelper.SendDirectMessage("Yosh", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
        //    //await SlackHelper.SendDirectMessage("Christine", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
        //    await base.ScheduleJob(cancellationToken);
        //}
    }
}
