using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface ISpielerService
    {
        Task<IEnumerable<Spieler>> GetAlleSpielerAsync();
    }
}
