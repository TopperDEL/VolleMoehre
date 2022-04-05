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
    public class OrteService : ServiceBase, Contracts.Interfaces.IOrteService
    {
        private string _apiKey;

        public OrteService(string apiKey)
        {
            _apiKey = apiKey;
            Barrel.ApplicationId = "vollemoehre_monkeycache";
        }

        public async Task<IEnumerable<Ort>> GetOrteAsync()
        {
#if !__WASM__
            if (!Barrel.Current.IsExpired("orte") || !IsOnline())
               return Barrel.Current.Get<IEnumerable<Ort>>("orte");
#endif

            var client = GetClient(_apiKey);
            try
            {
                var result = await client.GetAsync("orte", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsAsync<IEnumerable<VolleMoehre.Contracts.Model.Ort>>();

#if !__WASM__
                Barrel.Current.Add<IEnumerable<Ort>>("orte", resultContent, TimeSpan.FromDays(180));
#endif

                return resultContent;
            }
            catch (Exception ex)
            {

            }
            return new List<VolleMoehre.Contracts.Model.Ort>();
        }

        public async Task<MoehreResult> SaveOrtAsync(Ort ort)
        {
            var client = GetClient(_apiKey);
            try
            {
                var result = await client.PostAsJsonAsync("orte", ort).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
#if !__WASM__
                    Barrel.Current.Empty("orte");
#endif

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
