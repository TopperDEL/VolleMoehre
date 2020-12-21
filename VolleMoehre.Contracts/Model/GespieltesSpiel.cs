using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Model
{
    public class GespieltesSpiel
    {
        public string SpielId { get; set; }
        public string Vorgabe { get; set; }

        public long Reihenfolge { get; set; }

    }
}
