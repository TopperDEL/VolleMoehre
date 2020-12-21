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
    public class ListenController : InternalControllerBase
    {
        // GET api/auftritte/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<VolleMoehre.Contracts.Model.Liste>>> Get(string id)
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            ListenTyp listenTyp;
            if (!Enum.TryParse<ListenTyp>(id, out listenTyp))
                return NotFound();

            var listen = (await _store.GetAllAsync<VolleMoehre.Contracts.Model.Liste>(l => l.Typ == listenTyp)).ToList();
            if (listen == null)
                return NotFound();
            else
                return listen;
        }

        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] Liste value)
        {
            var success = await _store.SaveAsync(value);
            if (!success)
                return BadRequest();
            else
                return Ok();
        }
    }
}
