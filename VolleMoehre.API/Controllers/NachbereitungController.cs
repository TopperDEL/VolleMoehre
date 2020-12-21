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
    public class NachbereitungController : InternalControllerBase
    {
        // GET api/Nachbereitung        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auftrittstermin>>> Get()
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            DateTime border = DateTime.Now.AddYears(-1); //Nur die Auftritte der letzten 12 Monate
            if (border < DateTime.Parse("01.07.2015"))
                border = DateTime.Parse("01.07.2015");

            var auftritte = (await _store.GetAllAsync<Auftrittstermin>(t => border < t.Datum && t.Datum < DateTime.Now)).OrderByDescending(s => s.Datum);
            return auftritte.ToList();
        }

        // POST api/Nachbereitung
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] NacharbeitungsErgebnis value)
        {

            var existing = await _store.GetAsync<Auftrittstermin>(value.Id);
            if (existing == null)
                return BadRequest();
            if (!existing.NachbereitetVon.Contains(value.SpielerId))
                existing.NachbereitetVon.Add(value.SpielerId);
            if (value.GefahreneKilometer > 0)
            {
                foreach(var auslage in existing.Auslagen)
                {
                    if (auslage.SpielerId == value.SpielerId && auslage.GefahreneKilometer > 0)
                    {
                        existing.Auslagen.Remove(auslage);
                        break;
                    }
                }
                existing.Auslagen.Add(new Auslagen() { GefahreneKilometer = value.GefahreneKilometer, SpielerId = value.SpielerId });
            }

            existing.Abwesend.Remove(value.SpielerId);
            existing.Helfer.Remove(value.SpielerId);
            existing.Moderator.Remove(value.SpielerId);
            existing.Spieler.Remove(value.SpielerId);
            existing.Vorgemerkt.Remove(value.SpielerId);
            switch (value.SpielerStatus)
            {
                case Contracts.Interfaces.SpielerStatus.Abwesend:
                    existing.Abwesend.Add(value.SpielerId);
                    break;
                case Contracts.Interfaces.SpielerStatus.Helfer:
                    existing.Helfer.Add(value.SpielerId);
                    break;
                case Contracts.Interfaces.SpielerStatus.Moderator:
                    existing.Moderator.Add(value.SpielerId);
                    break;
                case Contracts.Interfaces.SpielerStatus.Spieler:
                    existing.Spieler.Add(value.SpielerId);
                    break;
                case Contracts.Interfaces.SpielerStatus.Vorgemerkt:
                    existing.Vorgemerkt.Add(value.SpielerId);
                    break;
                default:
                    return BadRequest();
            }
            
            var success = await _store.SaveAsync(existing);
            if (!success)
                return BadRequest();
            else
                return Ok();
        }
    }
}
