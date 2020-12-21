using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Contracts.Interfaces
{
    public enum SpielerStatus
    {
        Abwesend,
        Helfer,
        Spieler,
        Moderator,
        Vorgemerkt,
        Teilnehmer,
        Leiter
    }

    public interface IAuftritteService
    {
        Task<IEnumerable<AuftrittsterminPublic>> GetOeffentlicheAuftritteAsync();
        Task<IEnumerable<Auftrittstermin>> GetAlleAuftritteAsync();
        Task<MoehreResult> SaveAuftrittAsync(Auftrittstermin termin);
        Task<MoehreResult> DeleteAuftrittAsync(Auftrittstermin termin);
        Task<Auftrittstermin> GetAuftrittAsync(string id);
        Task<MoehreResult> ChangeStatusAsync(Spieler spieler, Auftrittstermin termin, SpielerStatus newSpielerStatus);
    }
}
