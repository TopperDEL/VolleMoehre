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
    public class AbrechnungsService : ServiceBase, IAbrechnungsAPIService
    {
        private string _apiKey;

        public AbrechnungsService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<List<Spielerabrechnung>> GetAbrechnungAsync(DateTime begda, DateTime endda)
        {
            var client = GetClient(_apiKey);
            try
            {
                var abrechnungsAnfrage = new AbrechnungsAbfrage();
                abrechnungsAnfrage.Beginn = begda;
                abrechnungsAnfrage.Ende = endda;

                var result = await client.PostAsJsonAsync("abrechnung", abrechnungsAnfrage).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadAsAsync<List<Spielerabrechnung>>();
            }
            catch (Exception ex)
            {

            }
            return new List<Spielerabrechnung>();
        }
    }
}
