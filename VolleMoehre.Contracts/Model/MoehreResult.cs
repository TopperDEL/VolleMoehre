using System;
using System.Collections.Generic;
using System.Text;

namespace VolleMoehre.Contracts.Model
{
    public class MoehreResult
    {
        public bool Erfolgreich { get; set; }
        public string Fehlermeldung { get; set; }

        public static MoehreResult WarErfolgreich()
        {
            return new MoehreResult() { Erfolgreich = true };
        }

        public static MoehreResult WarFehlerhaft(string fehlermeldung)
        {
            return new MoehreResult() { Erfolgreich = false, Fehlermeldung = fehlermeldung };
        }
    }
}
