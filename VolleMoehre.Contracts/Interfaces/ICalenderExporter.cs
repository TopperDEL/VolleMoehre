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
        string TransferToiCalFeed(Spieler spieler, List<Trainingstermin> trainingsTermine, List<Auftrittstermin> auftrittsTermine, List<Ort> orte);
        byte[] TransferToiCal(Auftrittstermin auftrittsTermin);
        byte[] TransferToiCalPublic(Auftrittstermin auftrittsTermin);
        byte[] TransferToiCal(Trainingstermin trainingsTermin);
    }
}
