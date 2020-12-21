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
    public class SpieleService : ServiceBase, Contracts.Interfaces.ISpieleService
    {
        private string _apiKey;

        public SpieleService(string apiKey)
        {
            _apiKey = apiKey;
            Barrel.ApplicationId = "vollemoehre_monkeycache";
        }

        public async Task<IEnumerable<Spiel>> GetSpieleAsync()
        {
            if (!Barrel.Current.IsExpired("spiele") || !IsOnline())
                return Barrel.Current.Get<IEnumerable<Spiel>>("spiele");

            var client = GetClient(_apiKey);
            try
            {
                var result = await client.GetAsync("spiele", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsAsync<IEnumerable<VolleMoehre.Contracts.Model.Spiel>>();

                Barrel.Current.Add<IEnumerable<Spiel>>("spiele", resultContent, TimeSpan.FromDays(180));

                return resultContent;
            }
            catch (Exception ex)
            {

            }
            return new List<VolleMoehre.Contracts.Model.Spiel>();
        }

        public async Task<MoehreResult> SaveSpielAsync(Spiel spiel)
        {
            var client = GetClient(_apiKey);
            try
            {
                var result = await client.PostAsJsonAsync("spiele", spiel).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    Barrel.Current.Empty("spiele");

                    return MoehreResult.WarErfolgreich();
                }
                else
                    return MoehreResult.WarFehlerhaft(result.ReasonPhrase);
            }
            catch (Exception ex)
            {
                return MoehreResult.WarFehlerhaft(ex.Message);
            }
        }
    }
}
