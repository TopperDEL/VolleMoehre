using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.Contracts.Model;
using VolleMoehre.App.Shared.ViewModels;
using VolleMoehre.Shared.Services;
using Plugin.Connectivity;

namespace VolleMoehre.App.Shared.Commands
{
    public class ChangeTrainingStatusCommand : ICommand
    {
        private SpielerStatus NewSpielerStatus;
        private TrainingsViewModel Model;

        public event EventHandler CanExecuteChanged;

        public ChangeTrainingStatusCommand(SpielerStatus newSpielerStatus, TrainingsViewModel model)
        {
            NewSpielerStatus = newSpielerStatus;
            Model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (!ServiceBase.IsOnline())
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            TrainingsterminViewModel model = parameter as TrainingsterminViewModel;
            model.Loading = true;
            model.LoadingFinished = false;

            ITrainingsService service = new TrainingsService(App.__APIKey);

            var success = await service.ChangeStatusAsync(App.__spieler, model.Termin, NewSpielerStatus);
            if(success.Erfolgreich)
            {
                var newTraining = await service.GetTrainingAsync(model.Termin.Id);
                model.RefreshFrom(newTraining, App.__spieler);

                var index = Model.Trainings.IndexOf(model);
                Model.Trainings.RemoveAt(index);
                Model.Trainings.Insert(index, model);
            }
            else
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(success.Fehlermeldung);
                await dialog.ShowAsync();
                return;
            }

            model.DoneLoading();
        }
    }
}
