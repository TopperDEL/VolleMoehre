using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.App.Shared.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<AuftrittsterminPublic> OeffentlicheAuftritte { get; set; }
        public int LogoTapCount { get; set; }

        public MainPageViewModel()
        {
            OeffentlicheAuftritte = new ObservableCollection<AuftrittsterminPublic>();
        }
    }
}
