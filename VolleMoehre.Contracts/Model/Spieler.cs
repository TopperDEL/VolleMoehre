using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Model
{
    public class Spieler
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string UserName { get; set; }
        public bool Aktiv { get; set; }
        public int OldId { get; set; }
        public string APISecret { get; set; }

        public Spieler()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
