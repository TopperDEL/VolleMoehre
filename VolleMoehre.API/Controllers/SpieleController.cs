using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpieleController : InternalControllerBase
    {
        // GET api/auftritte        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolleMoehre.Contracts.Model.Spiel>>> Get()
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var spiele = await _store.GetAllAsync<VolleMoehre.Contracts.Model.Spiel>();
            return spiele.ToList();
        }

        // GET api/auftritte/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VolleMoehre.Contracts.Model.Spiel>> Get(string id)
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var spiele = (await _store.GetAllAsync<VolleMoehre.Contracts.Model.Spiel>(a => a.Id == id)).FirstOrDefault();
            if (spiele == null)
                return NotFound();
            else
                return spiele;
        }

        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] Spiel value)
        {
            var success = await _store.SaveAsync(value);
            if (!success)
                return BadRequest();
            else
                return Ok();
        }
    }
}
