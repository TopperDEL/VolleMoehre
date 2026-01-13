using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.App.Shared.ViewModels;

namespace VolleMoehre.App.Shared.Commands
{
    public class SetSpielerCommand : ChangeStatusCommand
    {
        public SetSpielerCommand(AuftritteViewModel model) :base(SpielerStatus.Spieler, model)
        {

        }
    }
}
