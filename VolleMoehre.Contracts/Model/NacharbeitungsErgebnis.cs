using System;
using System.Collections.Generic;
using System.Text;
using VolleMoehre.Contracts.Interfaces;

namespace VolleMoehre.Contracts.Model
{
    public class NacharbeitungsErgebnis
    {
        public string Id { get; set; }
        public int GefahreneKilometer { get; set; }
        public SpielerStatus SpielerStatus { get; set; }
        public string SpielerId { get; set; }
    }
}
