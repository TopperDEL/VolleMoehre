using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Model
{
    public enum LogType
    {
        Normal = 0,
        Warning = 1,
        Error = 2
    }
    public class UserLog
    {
        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string LogMessage { get; set; }
        public LogType LogType { get; set; }
    }
}