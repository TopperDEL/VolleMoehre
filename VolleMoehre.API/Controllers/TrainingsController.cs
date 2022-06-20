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
    public class TrainingsController : InternalControllerBase
    {
        // GET api/auftritte        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolleMoehre.Contracts.Model.Trainingstermin>>> Get()
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var trainings = await _store.GetAllAsync<VolleMoehre.Contracts.Model.Trainingstermin>(a => a.Datum.Date >= DateTime.Now.Date);
            return trainings.ToList();
        }

        // GET api/auftritte/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VolleMoehre.Contracts.Model.Trainingstermin>> Get(string id)
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var training = (await _store.GetAllAsync<VolleMoehre.Contracts.Model.Trainingstermin>(a => a.Id == id)).FirstOrDefault();
            if (training == null)
                return NotFound();
            else
                return training;
        }

        // DELETE api/auftritte/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (!await IsInternalRequestAsync())
                return Forbid();

            var training = (await _store.GetAllAsync<VolleMoehre.Contracts.Model.Trainingstermin>(a => a.Id == id)).FirstOrDefault();
            if (training == null)
                return NotFound();
            else
            {
                var deleted = await _store.DeleteAsync<Trainingstermin>(training.Id);
                if (deleted)
                    return Ok();
                else
                    return BadRequest();
            }
        }

        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] Trainingstermin value)
        {
            var existing = await _store.GetAsync<Trainingstermin>(value.Id);

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
                existing.Datum = value.Datum;
                existing.Dauer = value.Dauer;
                existing.FreitextInfo = value.FreitextInfo;
                existing.Kommentare = value.Kommentare;
                existing.Trainingstyp = value.Trainingstyp;
                existing.OrtId = value.OrtId;

                var success = await _store.SaveAsync(existing);
                if (!success)
                    return BadRequest();
                else
                    return Ok();
            }
        }
    }
}
