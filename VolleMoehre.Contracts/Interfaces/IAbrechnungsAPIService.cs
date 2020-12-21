using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface IAbrechnungsAPIService
    {
        Task<List<Spielerabrechnung>> GetAbrechnungAsync(DateTime begda, DateTime endda);
    }
}
