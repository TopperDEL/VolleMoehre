using MonkeyCache.FileStore;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Shared.Services
{
    public class SpielerService : ServiceBase, Contracts.Interfaces.ISpielerService
    {
        private string _apiKey;

        public SpielerService(string apiKey)
        {
            _apiKey = apiKey;
            Barrel.ApplicationId = "vollemoehre_monkeycache";
        }

        public async Task<IEnumerable<Spieler>> GetAlleSpielerAsync()
        {
            if (!Barrel.Current.IsExpired("spieler") || !IsOnline())
                return Barrel.Current.Get<IEnumerable<Spieler>>("spieler");

            var client = GetClient(_apiKey);
            try
            {
                var result = await client.GetAsync("spieler", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsAsync<IEnumerable<VolleMoehre.Contracts.Model.Spieler>>();

                Barrel.Current.Add<IEnumerable<Spieler>>("spieler", resultContent, TimeSpan.FromDays(365));

                return resultContent;
            }
            catch (Exception ex)
            {

            }
            return new List<VolleMoehre.Contracts.Model.Spieler>();
        }
    }
}
