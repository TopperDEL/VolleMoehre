using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.App.Shared.ViewModels;

namespace VolleMoehre.App.Shared.Commands
{
    public class SetVorgemerktCommand : ChangeStatusCommand
    {
        public SetVorgemerktCommand(AuftritteViewModel model) :base(SpielerStatus.Vorgemerkt, model)
        {

        }
    }
}
