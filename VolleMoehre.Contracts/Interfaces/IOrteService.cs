using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface IOrteService
    {
        Task<IEnumerable<Ort>> GetOrteAsync();
        Task<MoehreResult> SaveOrtAsync(Ort ort);
    }
}
