using MonkeyCache.FileStore;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Shared.Services
{
    public class APIConnectionService : ServiceBase, IAPIConnectionService
    {
        public APIConnectionService()
        {
            Barrel.ApplicationId = "vollemoehre_monkeycache";
        }

        public async Task<Spieler> GetPlayerForAPIKey(string apiKey)
        {
            if (!Barrel.Current.IsExpired("verify_" + apiKey) | !IsOnline())
                return Barrel.Current.Get<Spieler>("verify_" + apiKey);

            var client = GetClient(apiKey);
            try
            {
                var result = await client.GetAsync("verify", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsAsync<VolleMoehre.Contracts.Model.Spieler>();

                Barrel.Current.Add<Spieler>("verify_" + apiKey, resultContent, TimeSpan.FromDays(365));

                return resultContent;
            }
            catch (Exception ex)
            {
#if __WASM__
                System.Console.WriteLine("*** Fehler: " + ex.Message);
#endif
            }
            return null;
        }
    }
}
