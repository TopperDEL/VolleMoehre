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
    public class AuftritteController : InternalControllerBase
    {
        // GET api/auftritte        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolleMoehre.Contracts.Model.Auftrittstermin>>> Get()
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var auftritte = await _store.GetAllAsync<VolleMoehre.Contracts.Model.Auftrittstermin>(a => a.Datum >= DateTime.Now);
            return auftritte.ToList();
        }

        // GET api/auftritte/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VolleMoehre.Contracts.Model.Auftrittstermin>> Get(string id)
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var auftritt = (await _store.GetAllAsync<VolleMoehre.Contracts.Model.Auftrittstermin>(a => a.Id == id)).FirstOrDefault();
            if (auftritt == null)
                return NotFound();
            else
                return auftritt;
        }

        // DELETE api/auftritte/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var auftritt = (await _store.GetAllAsync<VolleMoehre.Contracts.Model.Auftrittstermin>(a => a.Id == id)).FirstOrDefault();
            if (auftritt == null)
                return NotFound();
            else
            {
                var deleted = await _store.DeleteAsync<Auftrittstermin>(auftritt.Id);
                if (deleted)
                    return Ok();
                else
                    return BadRequest();
            }
        }

        // POST api/auftritte
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] Auftrittstermin value)
        {
            var existing = await _store.GetAsync<Auftrittstermin>(value.Id);

            if (existing == null)
            {
                var success = await _store.SaveAsync(value);
                if (!success)
                    return BadRequest();
                else
                    return Ok();
            }
            else
            {
                existing.BenoetigteSpieler = value.BenoetigteSpieler;
                existing.BezahlungHelfer = value.BezahlungHelfer;
                existing.BezahlungModerator = value.BezahlungModerator;
                existing.BezahlungSpieler = value.BezahlungSpieler;
                existing.Datum = value.Datum;
                existing.EintrittFrei = value.EintrittFrei;
                existing.EintrittInfo = value.EintrittInfo;
                existing.FreitextInfoExtern = value.FreitextInfoExtern;
                existing.FreitextInfoIntern = value.FreitextInfoIntern;
                existing.Gegnerlink = value.Gegnerlink;
                existing.Infolink = value.Infolink;
                existing.Musiker = value.Musiker;
                existing.Oeffentlich = value.Oeffentlich;
                existing.OrtId = value.OrtId;
                existing.Showtyp = value.Showtyp;
                existing.SpezialOrtText = value.SpezialOrtText;
                existing.SpezialTerminDescription = value.SpezialTerminDescription;
                existing.Ansprechpartner = value.Ansprechpartner;

                var success = await _store.SaveAsync(existing);
                if (!success)
                    return BadRequest();
                else
                    return Ok();
            }
        }
    }
}
