using EasyCronJob.Abstractions;
using Microsoft.Extensions.Logging;
using SlackAPI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VolleMoehre.API.Jobs
{
    public abstract class SlackHelper
    {
        public static Dictionary<string, string> Moehren = new Dictionary<string, string>()
        { 
            {"Tim","UJE17SRP1" },
            {"PhilippW","UJBHG5SRK" },
            {"PhilippH","UJ0MH67C3" },
            {"Philipp","UJ0MH67C3" },
            {"Christine","UJC4E8UKX" },
            {"Goeksen","UJ0MRNDKM" },
            {"Göksen","UJ0MRNDKM" },
            {"Yosh","UJ1DQHT5Y" },
            {"Kalli","UJPBU6XL6" },
            {"MichaelM","UJCV08X61" },
            {"Lisa","UJEEBPWSH" },
        };
        public static async Task SendMessage(string channel, string message)
        {
            string TOKEN = Environment.GetEnvironmentVariable("SLACK_API_TOKEN");
            var slackClient = new SlackTaskClient(TOKEN);

            var response = await slackClient.PostMessageAsync(channel, message).ConfigureAwait(true);
        }
        public static async Task SendDirectMessage(string moehre, string message)
        {
            string TOKEN = Environment.GetEnvironmentVariable("SLACK_API_TOKEN");
            var slackClient = new SlackTaskClient(TOKEN);

            var response = await slackClient.PostMessageAsync(Moehren[moehre], message).ConfigureAwait(true);
        }
    }
}
