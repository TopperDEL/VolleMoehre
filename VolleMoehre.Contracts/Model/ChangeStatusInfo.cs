using System;
using System.Collections.Generic;
using System.Text;
using VolleMoehre.Contracts.Interfaces;

namespace VolleMoehre.Contracts.Model
{
    public enum TargetType
    {
        Auftritt,
        Training
    }
    public class ChangeStatusInfo
    {
        public string TargetId { get; set; }
        public TargetType TargetType { get; set; }
        public string SpielerId { get; set; }
        public SpielerStatus NewStatus { get; set; }
    }
}
