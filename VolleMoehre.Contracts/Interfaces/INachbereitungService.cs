using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface INachbereitungService
    {
        Task<IEnumerable<Auftrittstermin>> GetToDoAsync();
        Task<bool> SaveNachbereitungAsync(NacharbeitungsErgebnis nachbereitung);
    }
}
