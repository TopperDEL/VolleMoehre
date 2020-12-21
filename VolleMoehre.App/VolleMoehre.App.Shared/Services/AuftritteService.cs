using MonkeyCache.FileStore;
using Plugin.Connectivity;
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
    public class AuftritteService : ServiceBase, Contracts.Interfaces.IAuftritteService
    {
        private string _apiKey;

        public AuftritteService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<MoehreResult> ChangeStatusAsync(Spieler spieler, Auftrittstermin termin, SpielerStatus newSpielerStatus)
        {
            var client = GetClient(_apiKey);
            try
            {
                var changeStatusInfo = new ChangeStatusInfo();
                changeStatusInfo.NewStatus = newSpielerStatus;
                changeStatusInfo.SpielerId = spieler.Id;
                changeStatusInfo.TargetId = termin.Id;
                changeStatusInfo.TargetType = TargetType.Auftritt;

                var result = await client.PostAsJsonAsync("spielerstatus", changeStatusInfo).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                    return MoehreResult.WarErfolgreich();
                else
                    return MoehreResult.WarFehlerhaft(result.ReasonPhrase);
            }
            catch (Exception ex)
            {
                return MoehreResult.WarFehlerhaft(ex.Message);
            }
        }

        public async Task<IEnumerable<Auftrittstermin>> GetAlleAuftritteAsync()
        {
            if (!IsOnline())
                return Barrel.Current.Get<List<Auftrittstermin>>("alleauftritte");

            var client = GetClient(_apiKey);
            try
            {
                var result = await client.GetAsync("auftritte", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsAsync<List<VolleMoehre.Contracts.Model.Auftrittstermin>>();
                resultContent = resultContent.OrderBy(a => a.Datum).ToList();
                foreach (var termin in resultContent)
                    termin.Uhrzeit = termin.Datum.TimeOfDay.ToString("hh\\:mm");

                Barrel.Current.Add<List<Auftrittstermin>>("alleauftritte", resultContent, TimeSpan.FromDays(180));

                return resultContent;
            }
            catch (Exception ex)
            {
            }
            return new List<VolleMoehre.Contracts.Model.Auftrittstermin>();
        }

        public async Task<IEnumerable<AuftrittsterminPublic>> GetOeffentlicheAuftritteAsync()
        {
            var client = GetClient();
            try
            {
                var result = await client.GetAsync("public/auftritte", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsAsync<IEnumerable<VolleMoehre.Contracts.Model.AuftrittsterminPublic>>();
                return resultContent.OrderBy(a => a.Datum);
            }
            catch (Exception ex)
            {

            }
            return new List<VolleMoehre.Contracts.Model.AuftrittsterminPublic>();
        }

        public async Task<MoehreResult> SaveAuftrittAsync(Auftrittstermin termin)
        {
            var client = GetClient(_apiKey);
            try
            {
                termin.Datum = termin.Datum.Date + TimeSpan.Parse(termin.Uhrzeit);
                var result = await client.PostAsJsonAsync("auftritte", termin).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    Barrel.Current.Empty("alleauftritte");

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

        public async Task<Auftrittstermin> GetAuftrittAsync(string terminId)
        {
            var client = GetClient(_apiKey);
            try
            {
                var result = await client.GetAsync("auftritte/"+terminId, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    var termin = await result.Content.ReadAsAsync<Auftrittstermin>();
                    termin.Uhrzeit = termin.Datum.ToString("hh\\:mm");
                    return termin;
                }
                else
                    return new Auftrittstermin();
            }
            catch (Exception ex)
            {

            }
            return new Auftrittstermin();
        }

        public async Task<MoehreResult> DeleteAuftrittAsync(Auftrittstermin termin)
        {
            var client = GetClient(_apiKey);
            try
            {
                termin.Datum = termin.Datum.Date + TimeSpan.Parse(termin.Uhrzeit);
                var result = await client.DeleteAsync("auftritte/" + termin.Id).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    Barrel.Current.Empty("alleauftritte");

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
