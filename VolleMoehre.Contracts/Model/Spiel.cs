using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Model
{
    public class Spiel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Beschreibung { get; set; }

        public Spiel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return Name;
        }

        public string ToString(string Vorgabe)
        {
            return Name + " (" + Vorgabe + ")";
        }
    }
}
