using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface IListenService
    {
        Task<IEnumerable<Liste>> GetListeAsync(ListenTyp listenTyp, bool forceRefresh = false);
        Task<MoehreResult> SaveListenEintragAsync(Liste liste);
    }
}
