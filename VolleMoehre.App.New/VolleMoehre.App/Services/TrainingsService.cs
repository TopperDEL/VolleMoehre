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
    public class TrainingsService : ServiceBase, Contracts.Interfaces.ITrainingsService
    {
        private string _apiKey;

        public TrainingsService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<MoehreResult> ChangeStatusAsync(Spieler spieler, Trainingstermin termin, SpielerStatus newSpielerStatus)
        {
            var client = GetClient(_apiKey);
            try
            {
                var changeStatusInfo = new ChangeStatusInfo();
                changeStatusInfo.NewStatus = newSpielerStatus;
                changeStatusInfo.SpielerId = spieler.Id;
                changeStatusInfo.TargetId = termin.Id;
                changeStatusInfo.TargetType = TargetType.Training;

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

        public async Task<Trainingstermin> GetTrainingAsync(string terminId)
        {
            var client = GetClient(_apiKey);
            try
            {
                var result = await client.GetAsync("trainings/" + terminId, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadAsAsync<Trainingstermin>();
                else
                    return new Trainingstermin();
            }
            catch (Exception ex)
            {

            }
            return new Trainingstermin();
        }

        public async Task<IEnumerable<Trainingstermin>> GetTrainingsAsync()
        {
#if !__WASM__
            if (!IsOnline())
                return Barrel.Current.Get<IEnumerable<Trainingstermin>>("alletrainings");
#endif

            var client = GetClient(_apiKey);
            try
            {
                var result = await client.GetAsync("trainings", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsAsync<IEnumerable<VolleMoehre.Contracts.Model.Trainingstermin>>();
                resultContent = resultContent.OrderBy(a => a.Datum).ToList();
                foreach (var termin in resultContent)
                    termin.Uhrzeit = termin.Datum.TimeOfDay.ToString("hh\\:mm");

#if !__WASM__
                Barrel.Current.Add<IEnumerable<Trainingstermin>>("alletrainings", resultContent, TimeSpan.FromDays(180));
#endif

                return resultContent;
            }
            catch (Exception ex)
            {

            }
            return new List<VolleMoehre.Contracts.Model.Trainingstermin>();
        }

        public async Task<MoehreResult> SaveTrainingAsync(Trainingstermin termin)
        {
            var client = GetClient(_apiKey);
            try
            {
                termin.Datum = termin.Datum.Date + TimeSpan.Parse(termin.Uhrzeit);
                var result = await client.PostAsJsonAsync("trainings", termin).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
#if !__WASM__
                    Barrel.Current.Empty("alletrainings");
#endif

                    return MoehreResult.WarErfolgreich();
                }
                else
                    return MoehreResult.WarFehlerhaft("Fehler beim Speichern: " + result.StatusCode);
            }
            catch (Exception ex)
            {
                return MoehreResult.WarFehlerhaft("Fehler beim Speichern: " + ex.Message);
            }
        }
    }
}
