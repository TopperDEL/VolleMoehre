using System;
using System.Collections.Generic;
using System.Linq;

namespace VolleMoehre.Contracts.Model
{
    public class Spielerabrechnung
    {
        public Spielerabrechnung()
        {
            Abrechnungszeilen = new List<string>();
            Endergebnis = 0;
            Spielername = string.Empty;
        }

        public string Spielername { get; set; }
        public List<string> Abrechnungszeilen { get; set; }
        public double Endergebnis { get; set; }
    }
}