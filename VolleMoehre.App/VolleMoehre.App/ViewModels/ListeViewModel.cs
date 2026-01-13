using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.App.Shared.ViewModels
{
    public class ListeViewModel : BaseViewModel
    {
        public ObservableCollection<Liste> ListenEintraege { get; set; }

        public ListeViewModel()
        {
            ListenEintraege = new ObservableCollection<Liste>();
        }
    }
}
