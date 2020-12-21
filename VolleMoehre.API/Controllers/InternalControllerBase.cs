using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VolleMoehre.API.Controllers
{
    public class InternalControllerBase:ControllerBase
    {
        public const string __headerKey = "vm-userkey";

        protected VolleMoehre.Contracts.Interfaces.IDBAdapter _store;
        protected List<VolleMoehre.Contracts.Model.Spieler> _spieler;

        public InternalControllerBase()
        {
            _store = new VolleMoehre.Adapter.LiteDB.LiteDBStore();
        }

        private async Task InitSpielerAsync()
        {
            _spieler = (await _store.GetAllAsync<VolleMoehre.Contracts.Model.Spieler>()).ToList();
        }

        protected async Task<bool> IsInternalRequestAsync()
        {
            if (_spieler == null)
                await InitSpielerAsync();

            if (!HttpContext.Request.Headers.ContainsKey(__headerKey))
            {
                return false;
            }
            else
            {
                string headerValue = HttpContext.Request.Headers[__headerKey];
                if (headerValue == null)
                    return false;

                foreach (var spieler in _spieler)
                {
                    if (spieler.APISecret == headerValue)
                        return true;
                }
                return false;
            }
        }
    }
}
