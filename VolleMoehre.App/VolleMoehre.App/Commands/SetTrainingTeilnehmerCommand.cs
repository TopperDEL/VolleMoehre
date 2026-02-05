using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.App.Shared.ViewModels;

namespace VolleMoehre.App.Shared.Commands
{
    public class SetTrainingTeilnehmerCommand : ChangeTrainingStatusCommand
    {
        public SetTrainingTeilnehmerCommand(TrainingsViewModel model) :base(SpielerStatus.Teilnehmer, model)
        {

        }
    }
}
