using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.App.Shared.ViewModels
{
    public class SpieleViewModel : BaseViewModel
    {
        public ObservableCollection<Spiel> Spiele { get; set; }

        public SpieleViewModel()
        {
            Spiele = new ObservableCollection<Spiel>();
        }
    }
}
