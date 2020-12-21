using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VolleMoehre.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : InternalControllerBase
    {
        // GET api/auftritte        
        [HttpGet]
        public async Task<ActionResult<VolleMoehre.Contracts.Model.Spieler>> Get()
        {
            try
            {
                if (!await IsInternalRequestAsync())
                    return Forbid();

                string apisecretFromRequest = Request.Headers[__headerKey];
                var spieler = (await _store.GetAllAsync<VolleMoehre.Contracts.Model.Spieler>(a => a.APISecret == apisecretFromRequest)).FirstOrDefault();
                if (spieler == null)
                    return NotFound();
                else
                    return spieler;
            }
            catch(Exception ex)
            {
                return new Contracts.Model.Spieler() { EMail = ex.Message };
            }
        }
    }
}
