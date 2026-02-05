using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VolleMoehre.App.Shared.ViewModels
{
    public class NachbereitungenViewModel : BaseViewModel
    {
        public ObservableCollection<AuftrittsterminViewModel> ToDos { get; set; }
        public NachbereitungenViewModel()
        {
            ToDos = new ObservableCollection<AuftrittsterminViewModel>();
        }
    }
}
