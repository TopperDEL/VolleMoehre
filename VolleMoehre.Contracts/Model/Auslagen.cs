using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Model
{
    public class Auslagen
    {
        public string SpielerId { get; set; }
        public int GefahreneKilometer { get; set; }
        public float AuslagenBetrag { get; set; }
        public string AuslagenBezeichnung { get; set; }

        public override string ToString()
        {
            string resultKm = string.Empty;
            string resultAuslagen = string.Empty;
            if (GefahreneKilometer > 0)
                resultKm = GefahreneKilometer + " km";
            
            if (AuslagenBetrag > 0)
                resultAuslagen = AuslagenBezeichnung + " (" + AuslagenBetrag + " €)";

            if (resultKm != string.Empty && resultAuslagen != string.Empty)
                return resultKm + " + " + resultAuslagen;
            else if (resultKm != string.Empty)
                return resultKm;
            else if (resultAuslagen != string.Empty)
                return resultAuslagen;

            return string.Empty;
        }
    }
}
