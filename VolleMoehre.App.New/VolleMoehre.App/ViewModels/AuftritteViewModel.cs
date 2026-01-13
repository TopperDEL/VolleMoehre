using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VolleMoehre.Contracts.Model;
using VolleMoehre.App.Shared.Commands;

namespace VolleMoehre.App.Shared.ViewModels
{
    
    public class AuftritteViewModel : BaseViewModel
    {
        public ObservableCollection<AuftrittsterminViewModel> Auftritte { get; set; }
        public SetSpielerCommand SetSpielerCommand { get; set; }
        public SetHelferCommand SetHelferCommand { get; set; }
        public SetModeratorCommand SetModeratorCommand { get; set; }
        public SetVorgemerktCommand SetVorgemerktCommand { get; set; }
        public SetAbwesendCommand SetAbwesendCommand { get; set; }

        public AuftritteViewModel()
        {
            Auftritte = new ObservableCollection<AuftrittsterminViewModel>();
            SetSpielerCommand = new SetSpielerCommand(this);
            SetHelferCommand = new SetHelferCommand(this);
            SetModeratorCommand = new SetModeratorCommand(this);
            SetVorgemerktCommand = new SetVorgemerktCommand(this);
            SetAbwesendCommand = new SetAbwesendCommand(this);
        }
    }
}
