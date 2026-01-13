using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Shared.Services
{
    public class NachbereitungService : ServiceBase, Contracts.Interfaces.INachbereitungService
    {
        private string _apiKey;

        public NachbereitungService(string apiKey)
        {
            _apiKey = apiKey;
        }

        
        public async Task<IEnumerable<Auftrittstermin>> GetToDoAsync()
        {
            var client = GetClient(_apiKey);
            try
            {
                var result = await client.GetAsync("nachbereitung", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsAsync<IEnumerable<VolleMoehre.Contracts.Model.Auftrittstermin>>();
                return resultContent.OrderByDescending(a => a.Datum);
            }
            catch (Exception ex)
            {
            }
            return new List<VolleMoehre.Contracts.Model.Auftrittstermin>();
        }

        public async Task<bool> SaveNachbereitungAsync(NacharbeitungsErgebnis ergebnis)
        {
            var client = GetClient(_apiKey);
            try
            {
                var result = await client.PostAsJsonAsync("nachbereitung", ergebnis).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
