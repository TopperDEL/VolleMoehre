using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.App.Shared.ViewModels;

namespace VolleMoehre.App.Shared.Commands
{
    public class SetTrainingAbwesendCommand : ChangeTrainingStatusCommand
    {
        public SetTrainingAbwesendCommand(TrainingsViewModel model) :base(SpielerStatus.Abwesend, model)
        {

        }
    }
}
