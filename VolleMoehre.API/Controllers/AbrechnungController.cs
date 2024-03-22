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
    public class AbrechnungController : InternalControllerBase
    {
        [HttpPost]
        public async Task<List<Spielerabrechnung>> Post([FromBody] AbrechnungsAbfrage value)
        {
            return (await RechneAbAsync(value.Beginn, value.Ende, new VolleMoehre.Adapter.LiteDB.LiteDBStore())).ToList();
        }

        public async Task<IEnumerable<Spielerabrechnung>> RechneAbAsync(DateTime begda, DateTime endda, VolleMoehre.Contracts.Interfaces.IDBAdapter store)
        {
            List<Spielerabrechnung> abrechnungsListe = new List<Spielerabrechnung>();

            List<Spieler> alleSpieler = (await store.GetAllAsync<Spieler>()).Where(s => s.Aktiv).ToList();
            List<Auftrittstermin> termine = (await store.GetAllAsync<Auftrittstermin>(t => begda <= t.Datum && t.Datum <= endda)).ToList();

            foreach (Spieler spieler in alleSpieler)
            {
                Spielerabrechnung abrechnung = new Spielerabrechnung();
                abrechnung.Spielername = spieler.Name;

                //Termine lesen
                termine = termine.OrderBy(t => t.Datum).ToList();
                foreach (Auftrittstermin termin in termine)
                {
                    var ort = await store.GetAsync<Ort>(termin.OrtId.ToString());
                    if (termin.Helfer.Contains(spieler.Id))
                    {
                        abrechnung.Abrechnungszeilen.Add(termin.Showtyp + " - " + ort.Bezeichnung + " - " + termin.Datum.Date.ToShortDateString() + " - Helfer - " + termin.BezahlungHelfer + "€");
                        abrechnung.Endergebnis += termin.BezahlungHelfer;
                    }
                    else if (termin.Spieler.Contains(spieler.Id))
                    {
                        abrechnung.Abrechnungszeilen.Add(termin.Showtyp + " - " + ort.Bezeichnung + " - " + termin.Datum.Date.ToShortDateString() + " - Spieler - " + termin.BezahlungSpieler + "€");
                        abrechnung.Endergebnis += termin.BezahlungSpieler;
                    }
                    else if (termin.Moderator.Contains(spieler.Id))
                    {
                        abrechnung.Abrechnungszeilen.Add(termin.Showtyp + " - " + ort.Bezeichnung + " - " + termin.Datum.Date.ToShortDateString() + " - Moderation - " + termin.BezahlungModerator + "€");
                        abrechnung.Endergebnis += termin.BezahlungModerator;
                    }
                    foreach (Auslagen auslage in termin.Auslagen.Where(a => a.SpielerId == spieler.Id))
                    {
                        if (auslage.GefahreneKilometer != 0)
                        {
                            double KMWert = ((float)auslage.GefahreneKilometer) * 0.30d;
                            KMWert = Math.Round(KMWert, 2);
                            abrechnung.Abrechnungszeilen.Add("    + Gefahrene Kilometer: " + auslage.GefahreneKilometer + " - " + KMWert + "€");
                            abrechnung.Endergebnis += KMWert;
                        }
                        if (auslage.AuslagenBetrag != 0)
                        {
                            abrechnung.Abrechnungszeilen.Add("    + " + auslage.AuslagenBezeichnung + ": " + auslage.AuslagenBetrag + "€");
                            abrechnung.Endergebnis += auslage.AuslagenBetrag;
                        }
                    }
                }
                abrechnungsListe.Add(abrechnung);
            }
            return abrechnungsListe;
        }
    }
}
