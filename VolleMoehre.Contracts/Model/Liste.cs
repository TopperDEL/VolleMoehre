using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Model
{
    public enum ListenTyp
    {
        Abfragen,
        Genres,
        Maerchen,
        Beziehungen,
        Charaktere,
        Gefuehle,
        Ticks,
        Musikstile,
        SpielelisteKinderimpro,
        SpielelisteKlassiker,
        SpielelisteImprokabarett,
        SpielelisteMatch,
        SpielelisteOpenAir,
        ForderungenMatch,
        MoehreKontaktdaten,
        SonstigeKontaktdaten
    }
    public class Liste
    {
        public string Id { get; set; }
        public ListenTyp Typ { get; set; }
        public string Name { get; set; }
        public string Beschreibung { get; set; }

        public Liste()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
