using VolleMoehre.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface IApplicationUser
    {
        string UserName { get; set; }
        string Email { get; set; }
    }
}
