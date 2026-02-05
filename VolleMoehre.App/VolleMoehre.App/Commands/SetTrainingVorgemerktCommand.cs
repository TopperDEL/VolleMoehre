using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.App.Shared.ViewModels;

namespace VolleMoehre.App.Shared.Commands
{
    public class SetTrainingVorgemerktCommand : ChangeTrainingStatusCommand
    {
        public SetTrainingVorgemerktCommand(TrainingsViewModel model) :base(SpielerStatus.Vorgemerkt, model)
        {

        }
    }
}
