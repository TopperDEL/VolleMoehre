using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace VolleMoehre.Web.Pages
{
    public class IndexModel : PageModel
    {
        public List<Contracts.Model.AuftrittsterminPublic> Auftritte;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var client = new RestClient("https://api.vollemoehre.de/api/");
            var request = new RestRequest("public/auftritte", DataFormat.Json);
            var response = client.Get<List<Contracts.Model.AuftrittsterminPublic>>(request);
            Auftritte = response.Data.OrderBy(a=>a.Datum).ToList();
        }
    }
}
