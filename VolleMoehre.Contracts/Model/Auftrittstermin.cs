using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Model
{
    public class Auftrittstermin
    {
        public string Id { get; set; }
        public DateTime Datum { get; set; }
        public string Uhrzeit { get; set; }
        public string Showtyp { get; set; }
        public bool Oeffentlich { get; set; }
        public bool EintrittFrei { get; set; }
        public string EintrittInfo { get; set; }
        public string Musiker { get; set; }
        public string Gegnerlink { get; set; }
        public string Infolink { get; set; }
        public long BenoetigteSpieler { get; set; }
        public long BezahlungModerator { get; set; }
        public long BezahlungSpieler { get; set; }
        public long BezahlungHelfer { get; set; }
        public string FreitextInfoIntern { get; set; }
        public string FreitextInfoExtern { get; set; }
        public string SpezialTerminDescription { get; set; }
        public string SpezialOrtText { get; set; }

        public List<Kommentar> Kommentare { get; set; }

        public string OrtId { get; set; }
        public List<string> Spieler { get; set; }
        public List<string> Moderator { get; set; }
        public List<string> Helfer { get; set; }
        public List<string> Vorgemerkt { get; set; }
        public List<string> Abwesend { get; set; }

        public bool FehlendeModerationVerkuendet { get; set; }
        public bool FehlendeSpielerVerkuendet { get; set; }
        public bool ModeratorInformiert { get; set; }
        public bool SpielerInformiert { get; set; }
        public bool NachbereitungModerationAngefordert { get; set; }
        public bool NachbereitungAbrechnungAngefordert { get; set; }
        public bool PresseInfoVersendet { get; set; }
        public DateTime ZuletztAnFehlendeAussageErinnert { get; set; }
        public DateTime ZuletztAnAuftrittErinnert { get; set; }
        public bool AnFehlendenMusikerErinnert{ get; set; }
        public bool NichtOeffentlichErinnert { get; set; }

        public List<string> NachbereitetVon { get; set; }

        public List<Auslagen> Auslagen { get; set; }
        public List<GespieltesSpiel> GespielteSpiele { get; set; }

        public Auftrittstermin()
        {
            Id = Guid.NewGuid().ToString();
            Kommentare = new List<Kommentar>();
            Spieler = new List<string>();
            Moderator = new List<string>();
            Helfer = new List<string>();
            Vorgemerkt = new List<string>();
            Abwesend = new List<string>();
            Auslagen = new List<Auslagen>();
            GespielteSpiele = new List<GespieltesSpiel>();
            NachbereitetVon = new List<string>();

            Uhrzeit = "20:00";
        }
    }
}
