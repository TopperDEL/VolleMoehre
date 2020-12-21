using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface ICalenderExporter
    {
        byte[] TransferToiCalFeed(List<Spieler> alleApieler, Spieler spieler, List<Trainingstermin> trainingsTermine, List<Auftrittstermin> auftrittsTermine);
        byte[] TransferToiCal(Auftrittstermin auftrittsTermin);
        byte[] TransferToiCalPublic(Auftrittstermin auftrittsTermin);
        byte[] TransferToiCal(Trainingstermin trainingsTermin);
    }
}
