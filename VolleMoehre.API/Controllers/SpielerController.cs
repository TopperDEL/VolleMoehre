using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VolleMoehre.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpielerController : InternalControllerBase
    {
        // GET api/auftritte        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolleMoehre.Contracts.Model.Spieler>>> Get()
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var spieler = await _store.GetAllAsync<VolleMoehre.Contracts.Model.Spieler>();
            return spieler.ToList();
        }

        // GET api/auftritte/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VolleMoehre.Contracts.Model.Spieler>> Get(string id)
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var spieler = (await _store.GetAllAsync<VolleMoehre.Contracts.Model.Spieler>(a => a.Id == id && a.APISecret == Request.Headers[__headerKey])).FirstOrDefault();
            if (spieler == null)
                return NotFound();
            else
                return spieler;
        }
    }
}
