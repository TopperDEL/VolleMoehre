using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Model
{
    public class Ort
    {
        public string Id { get; set; }
        public string Bezeichnung { get; set; }
        public string Strasse { get; set; }
        public string Hausnummer { get; set; }
        public string Postleitzahl { get; set; }
        public string Stadt { get; set; }
        public string Bemerkung { get; set; }
        public string Eintritt { get; set; }
        public string VVKLink { get; set; }
        public string AnfahrtLink { get; set; }
        public string InfoLink { get; set; }

        public Ort()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Stadt))
            {
                return Bezeichnung + ", " + Stadt;
            }

            if (!string.IsNullOrEmpty(Bemerkung))
            {
                return Bezeichnung + " (" + Bemerkung + ")";
            }
            return Bezeichnung;
        }
    }
}
