using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.App.Shared.ViewModels;

namespace VolleMoehre.App.Shared.Commands
{
    public class SetModeratorCommand : ChangeStatusCommand
    {
        public SetModeratorCommand(AuftritteViewModel model) :base(SpielerStatus.Moderator, model)
        {

        }
    }
}
