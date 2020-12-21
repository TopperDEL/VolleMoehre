using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.API.Controllers
{
    [Route("api/public/auftritte")]
    [ApiController]
    public class Public_AuftritteController : ControllerBase
    {
        private VolleMoehre.Contracts.Interfaces.IDBAdapter _store;

        public Public_AuftritteController()
        {
            _store = new VolleMoehre.Adapter.LiteDB.LiteDBStore();
        }

        // GET api/public/auftritte        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolleMoehre.Contracts.Model.AuftrittsterminPublic>>> Get()
        {
            try
            {
                var auftritte = await _store.GetAllAsync<VolleMoehre.Contracts.Model.Auftrittstermin>(a => a.Oeffentlich && a.Datum >= DateTime.Now);
                List<VolleMoehre.Contracts.Model.AuftrittsterminPublic> retList = new List<Contracts.Model.AuftrittsterminPublic>();
                foreach (var auftritt in auftritte)
                {
                    var retAuftritt = new Contracts.Model.AuftrittsterminPublic()
                    {
                        Id = auftritt.Id,
                        Datum = auftritt.Datum,
                        EintrittFrei = auftritt.EintrittFrei,
                        EintrittInfo = auftritt.EintrittInfo,
                        FreitextInfoExtern = auftritt.FreitextInfoExtern,
                        Gegnerlink = auftritt.Gegnerlink,
                        Infolink = auftritt.Infolink,
                        Showtyp = auftritt.Showtyp,
                        Spielort = auftritt.SpezialOrtText
                    };
                    foreach (var spieler in auftritt.Spieler)
                        retAuftritt.Teilnehmer.Add((await _store.GetAsync<Spieler>(spieler)).Name);
                    foreach (var spieler in auftritt.Moderator)
                        retAuftritt.Teilnehmer.Add((await _store.GetAsync<Spieler>(spieler)).Name);
                    foreach (var spieler in auftritt.Helfer)
                        retAuftritt.Teilnehmer.Add((await _store.GetAsync<Spieler>(spieler)).Name);
                    if (string.IsNullOrEmpty(auftritt.SpezialOrtText))
                    {
                        var ort = await _store.GetAsync<Ort>(auftritt.OrtId);
                        if (ort != null)
                        {
                            retAuftritt.Spielort = ort.Bezeichnung;
                            retAuftritt.Hausnummer = ort.Hausnummer;
                            retAuftritt.Postleitzahl = ort.Postleitzahl;
                            retAuftritt.Ort = ort.Stadt;
                            retAuftritt.AnfahrtLink = ort.AnfahrtLink;
                            retAuftritt.Infolink = ort.InfoLink;
                            retAuftritt.VVKLink = ort.VVKLink;
                            if (string.IsNullOrEmpty(retAuftritt.EintrittInfo))
                                retAuftritt.EintrittInfo = ort.Eintritt;
                        }
                        else
                            retAuftritt.Ort = "nähere Informationen folgen";
                    }
                    if (!string.IsNullOrEmpty(auftritt.SpezialTerminDescription))
                        retAuftritt.Showtyp = auftritt.SpezialTerminDescription;


                    retList.Add(retAuftritt);
                }
                return retList;
            }
            catch (Exception ex)
            {
                var ret = new List<Contracts.Model.AuftrittsterminPublic>();
                ret.Add(new AuftrittsterminPublic() { Showtyp = ex.Message });
                return ret;
            }
        }

        // GET api/public/auftritte/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VolleMoehre.Contracts.Model.AuftrittsterminPublic>> Get(string id)
        {
            var auftritt = (await _store.GetAllAsync<VolleMoehre.Contracts.Model.Auftrittstermin>(a => a.Oeffentlich && a.Id == id)).FirstOrDefault();
            if (auftritt == null)
                return NotFound();

            var result = new Contracts.Model.AuftrittsterminPublic()
            {
                Id = auftritt.Id,
                Datum = auftritt.Datum,
                EintrittFrei = auftritt.EintrittFrei,
                EintrittInfo = auftritt.EintrittInfo,
                FreitextInfoExtern = auftritt.FreitextInfoExtern,
                Gegnerlink = auftritt.Gegnerlink,
                Infolink = auftritt.Infolink,
                Showtyp = auftritt.Showtyp,
                Ort = auftritt.SpezialOrtText
            };

            if (string.IsNullOrEmpty(auftritt.SpezialOrtText))
            {
                result.Ort = (await _store.GetAsync<Ort>(auftritt.OrtId)).Bezeichnung;
            }
            if (!string.IsNullOrEmpty(auftritt.SpezialTerminDescription))
                result.Showtyp = auftritt.SpezialTerminDescription;

            return result;
        }
    }
}
