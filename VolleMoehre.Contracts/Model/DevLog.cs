using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Model
{
    public class DevLog
    {
        public string LogMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime Timestamp { get; set; }
    }
}