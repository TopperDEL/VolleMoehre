using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface IAbrechnungsService
    {
        Task<IEnumerable<Spielerabrechnung>> RechneAbAsync(DateTime begda, DateTime endda, IDBAdapter dataBase);
    }
}
