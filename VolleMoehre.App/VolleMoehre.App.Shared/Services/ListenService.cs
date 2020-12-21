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
    public class ListenService : ServiceBase, Contracts.Interfaces.IListenService
    {
        private string _apiKey;

        public ListenService(string apiKey)
        {
            _apiKey = apiKey;
            Barrel.ApplicationId = "vollemoehre_monkeycache";
        }

        public async Task<IEnumerable<Liste>> GetListeAsync(ListenTyp listenTyp, bool forceRefresh = false)
        {
            if(forceRefresh)
                Barrel.Current.Empty("liste_" + listenTyp.ToString());

            if (!Barrel.Current.IsExpired("liste_" + listenTyp.ToString()) || !IsOnline())
                return Barrel.Current.Get<IEnumerable<Liste>>("liste_" + listenTyp.ToString());

            var client = GetClient(_apiKey);
            try
            {
                var result = await client.GetAsync("listen/" + listenTyp.ToString(), HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsAsync<List<VolleMoehre.Contracts.Model.Liste>>();

                Barrel.Current.Add<IEnumerable<Liste>>("liste_" + listenTyp.ToString(), resultContent, TimeSpan.FromDays(180));

                return resultContent;
            }
            catch (Exception ex)
            {

            }
            return new List<VolleMoehre.Contracts.Model.Liste>();
        }

        public async Task<MoehreResult> SaveListenEintragAsync(Liste eintrag)
        {
            var client = GetClient(_apiKey);
            try
            {
                var result = await client.PostAsJsonAsync("listen", eintrag).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    Barrel.Current.Empty("liste_" + eintrag.Typ.ToString());

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
