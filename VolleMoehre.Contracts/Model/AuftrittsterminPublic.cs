using System;
using System.Collections.Generic;
using System.Text;

namespace VolleMoehre.Contracts.Model
{
    public class AuftrittsterminPublic
    {
        public string Id { get; set; }
        public DateTimeOffset Datum { get; set; }
        public string Showtyp { get; set; }
        public bool EintrittFrei { get; set; }
        public string EintrittInfo { get; set; }
        public string Gegnerlink { get; set; }
        public string Infolink { get; set; }
        public string FreitextInfoExtern { get; set; }
        public string Spielort { get; set; }
        public string Strasse { get; set; }
        public string Hausnummer { get; set; }
        public string Postleitzahl { get; set; }
        public string Ort { get; set; }
        public string AnfahrtLink { get; set; }
        public string VVKLink { get; set; }
        public List<string> Teilnehmer { get; set; }

        public AuftrittsterminPublic()
        {
            Id = string.Empty;
            Teilnehmer = new List<string>();
        }
    }
}
