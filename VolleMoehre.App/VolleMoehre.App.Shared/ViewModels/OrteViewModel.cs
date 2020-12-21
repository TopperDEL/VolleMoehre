using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.App.Shared.ViewModels
{
    public class OrteViewModel : BaseViewModel
    {
        public ObservableCollection<Ort> Orte { get; set; }

        public OrteViewModel()
        {
            Orte = new ObservableCollection<Ort>();
        }
    }
}
