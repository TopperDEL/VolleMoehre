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
    public class OrteController : InternalControllerBase
    {
        // GET api/auftritte        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolleMoehre.Contracts.Model.Ort>>> Get()
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var orte = await _store.GetAllAsync<VolleMoehre.Contracts.Model.Ort>();
            return orte.ToList();
        }

        // GET api/auftritte/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VolleMoehre.Contracts.Model.Ort>> Get(string id)
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var ort = (await _store.GetAllAsync<VolleMoehre.Contracts.Model.Ort>(a => a.Id == id)).FirstOrDefault();
            if (ort == null)
                return NotFound();
            else
                return ort;
        }

        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] Ort value)
        {
            var success = await _store.SaveAsync(value);
            if (!success)
                return BadRequest();
            else
                return Ok();
        }
    }
}
