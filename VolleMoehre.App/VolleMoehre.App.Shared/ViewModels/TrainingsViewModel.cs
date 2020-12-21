using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VolleMoehre.Contracts.Model;
using VolleMoehre.App.Shared.Commands;

namespace VolleMoehre.App.Shared.ViewModels
{
    public class TrainingsViewModel : BaseViewModel
    {
        public ObservableCollection<TrainingsterminViewModel> Trainings { get; set; }
        public SetTrainingTeilnehmerCommand SetTeilnehmerCommand { get; set; }
        public SetTrainingLeiterCommand SetLeiterCommand { get; set; }
        public SetTrainingVorgemerktCommand SetVorgemerktCommand { get; set; }
        public SetTrainingAbwesendCommand SetAbwesendCommand { get; set; }

        public TrainingsViewModel()
        {
            Trainings = new ObservableCollection<TrainingsterminViewModel>();
            SetTeilnehmerCommand = new SetTrainingTeilnehmerCommand(this);
            SetLeiterCommand = new SetTrainingLeiterCommand(this);
            SetVorgemerktCommand = new SetTrainingVorgemerktCommand(this);
            SetAbwesendCommand = new SetTrainingAbwesendCommand(this);
        }
    }
}
