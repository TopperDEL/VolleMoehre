using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VolleMoehre.Contracts.Model
{
    public class AuftrittsterminBackup
    {
        public string Id { get; set; }
        public DateTime SaveDate { get; set; }
        public Auftrittstermin TerminVorher { get; set; }
        public Auftrittstermin TerminNachher { get; set; }
        public string Aenderer { get; set; }

        public AuftrittsterminBackup()
        {
            Id = string.Empty;
        }
    }
}
