using EasyCronJob.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VolleMoehre.API.Jobs
{
    public class TimTest : CronJobService
    {
        public TimTest(ICronConfiguration<TrainingsReminderJob> cronConfiguration)
            : base(cronConfiguration.CronExpression, cronConfiguration.TimeZoneInfo, cronConfiguration.CronFormat)
        {
        }

        //public async override Task DoWork(CancellationToken cancellationToken)
        protected async override Task ScheduleJob(CancellationToken cancellationToken)
        {
            //await SlackHelper.SendDirectMessage("Tim", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
            //await SlackHelper.SendDirectMessage("Goeksen", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
            //await SlackHelper.SendDirectMessage("Michael", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
            //await SlackHelper.SendDirectMessage("PhilippW", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
            //await SlackHelper.SendDirectMessage("PhilippH", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
            //await SlackHelper.SendDirectMessage("Lisa", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
            //await SlackHelper.SendDirectMessage("Kalli", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
            //await SlackHelper.SendDirectMessage("Yosh", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
            //await SlackHelper.SendDirectMessage("Christine", "Hallo! Ich bin das Erinnerungsmöhrchen. Über diesen Kanal sende ich Erinnerungen, die dich persönlich betreffen - etwa wenn für dich ein Auftritt ansteht oder du noch keine Aussage zu einem Termin getätigt hast. :)");
        }
    }
}
