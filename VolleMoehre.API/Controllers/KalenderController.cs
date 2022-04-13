using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.API.Controllers
{
    [Route("api/public/kalender")]
    [ApiController]
    public class KalenderController : ControllerBase
    {
        private VolleMoehre.Contracts.Interfaces.IDBAdapter _store;
        private VolleMoehre.Contracts.Interfaces.ICalenderExporter _calenderExporter;

        public KalenderController()
        {
            _store = new VolleMoehre.Adapter.LiteDB.LiteDBStore();
            _calenderExporter = new Adapter.Calender.CalenderExportService();
        }

        // GET api/public/kalender        [HttpGet]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var alleSpieler = await _store.GetAllAsync<Spieler>();
                var spieler = alleSpieler.Where(s => s.UserName == id.Replace(".ics","").ToLower()).FirstOrDefault();
                var auftritte = await _store.GetAllAsync<Auftrittstermin>(a => a.Oeffentlich && a.Datum >= DateTime.Now);
                var trainings = await _store.GetAllAsync<Trainingstermin>(a => a.Datum >= DateTime.Now);

                var calBytes = _calenderExporter.TransferToiCalFeed(spieler, trainings.ToList(), auftritte.ToList());

                return File(System.Text.Encoding.UTF8.GetBytes(calBytes), "text/calendar", "Kalender.ics");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
