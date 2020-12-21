using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface ITrainingsService
    {
        Task<IEnumerable<Trainingstermin>> GetTrainingsAsync();
        Task<MoehreResult> SaveTrainingAsync(Trainingstermin termin);
        Task<Trainingstermin> GetTrainingAsync(string id);
        Task<MoehreResult> ChangeStatusAsync(Spieler spieler, Trainingstermin termin, SpielerStatus newSpielerStatus);
    }
}
