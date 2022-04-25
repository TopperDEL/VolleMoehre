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
    public class SpielerStatusController : InternalControllerBase
    {
        // POST api/auftritte
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] ChangeStatusInfo value)
        {
            if (value.TargetType == TargetType.Auftritt)
            {
                var auftritt = await _store.GetAsync<Auftrittstermin>(value.TargetId);
                if (auftritt != null)
                {
                    auftritt.Abwesend.Remove(value.SpielerId);
                    auftritt.Helfer.Remove(value.SpielerId);
                    auftritt.Moderator.Remove(value.SpielerId);
                    auftritt.Spieler.Remove(value.SpielerId);
                    auftritt.Vorgemerkt.Remove(value.SpielerId);
                    switch (value.NewStatus)
                    {
                        case Contracts.Interfaces.SpielerStatus.Abwesend:
                            auftritt.Abwesend.Add(value.SpielerId);
                            break;
                        case Contracts.Interfaces.SpielerStatus.Helfer:
                            auftritt.Helfer.Add(value.SpielerId);
                            break;
                        case Contracts.Interfaces.SpielerStatus.Moderator:
                            auftritt.Moderator.Add(value.SpielerId);
                            break;
                        case Contracts.Interfaces.SpielerStatus.Spieler:
                            auftritt.Spieler.Add(value.SpielerId);
                            break;
                        case Contracts.Interfaces.SpielerStatus.Vorgemerkt:
                            auftritt.Vorgemerkt.Add(value.SpielerId);
                            break;
                        default:
                            return BadRequest();
                    }

                    var success = await _store.SaveAsync(auftritt);
                    if (success)
                        return Ok();
                }
            }
            else if (value.TargetType == TargetType.Training)
            {
                var training = await _store.GetAsync<Trainingstermin>(value.TargetId);
                if (training != null)
                {
                    training.Abwesend.Remove(value.SpielerId);
                    training.Leiter.Remove(value.SpielerId);
                    training.Vorgemerkt.Remove(value.SpielerId);
                    training.Online.Remove(value.SpielerId);
                    training.Teilnehmer.Remove(value.SpielerId);
                    switch (value.NewStatus)
                    {
                        case Contracts.Interfaces.SpielerStatus.Abwesend:
                            training.Abwesend.Add(value.SpielerId);
                            break;
                        case Contracts.Interfaces.SpielerStatus.Leiter:
                            training.Leiter.Add(value.SpielerId);
                            break;
                        case Contracts.Interfaces.SpielerStatus.Teilnehmer:
                            training.Teilnehmer.Add(value.SpielerId);
                            break;
                        case Contracts.Interfaces.SpielerStatus.Vorgemerkt:
                            training.Vorgemerkt.Add(value.SpielerId);
                            break;
                        case Contracts.Interfaces.SpielerStatus.Online:
                            training.Online.Add(value.SpielerId);
                            break;
                        default:
                            return BadRequest();
                    }

                    var success = await _store.SaveAsync(training);
                    if (success)
                        return Ok();
                }
            }

            return BadRequest();
        }
    }
}
