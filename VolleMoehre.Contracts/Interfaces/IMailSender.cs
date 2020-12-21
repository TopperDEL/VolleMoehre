using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface IMailSender
    {
        bool CreateAndSend(string from, string to, string subject, string html);
    }
}
