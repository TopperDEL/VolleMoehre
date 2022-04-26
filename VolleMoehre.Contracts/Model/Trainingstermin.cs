using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Model
{
    public enum Trainingstypen
    {
        Training,
        Stammtisch,
        Workshop,
        Hütte
    }
    public class Trainingstermin
    {
        public string Id { get; set; }
        public DateTime Datum { get; set; }
        public string Uhrzeit { get; set; }
        public int Dauer { get; set; }
        public string FreitextInfo { get; set; }
        public Trainingstypen Trainingstyp { get; set; }

        public List<Kommentar> Kommentare { get; set; }

        public string OrtId { get; set; }
        public List<string> Teilnehmer { get; set; }
        public List<string> Leiter { get; set; }
        public List<string> Abwesend { get; set; }
        public List<string> Vorgemerkt { get; set; }
        public List<string> Online { get; set; }

        public bool FehlenderLeiterVerkuendet { get; set; }
        public bool ZuwenigMoehrenVerkuendet { get; set; }
        public DateTime ZuletztAnFehlendeAussageErinnert { get; set; }
        public DateTime ZuletztAnTrainingErinnert { get; set; }

        public Trainingstermin()
        {
            Id = Guid.NewGuid().ToString();
            Teilnehmer = new List<string>();
            Leiter = new List<string>();
            Abwesend = new List<string>();
            Vorgemerkt = new List<string>();
            Online = new List<string>();
            Kommentare = new List<Kommentar>();

            Uhrzeit = "19:00";
            Dauer = 120;
        }
    }
}
