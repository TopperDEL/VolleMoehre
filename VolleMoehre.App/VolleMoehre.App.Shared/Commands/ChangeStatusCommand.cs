using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.Contracts.Model;
using VolleMoehre.App.Shared.ViewModels;
using VolleMoehre.Shared.Services;

namespace VolleMoehre.App.Shared.Commands
{
    public class ChangeStatusCommand : ICommand
    {
        private SpielerStatus NewSpielerStatus;
        private AuftritteViewModel Model;

        public event EventHandler CanExecuteChanged;

        public ChangeStatusCommand(SpielerStatus newSpielerStatus, AuftritteViewModel model)
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

            AuftrittsterminViewModel model = parameter as AuftrittsterminViewModel;
            model.Loading = true;
            model.LoadingFinished = false;

            IAuftritteService service = new AuftritteService(App.__APIKey);

            var success = await service.ChangeStatusAsync(App.__spieler, model.Termin, NewSpielerStatus);
            if (success.Erfolgreich)
            {
                if (NewSpielerStatus == SpielerStatus.Abwesend || NewSpielerStatus == SpielerStatus.Vorgemerkt)
                {
                    if (model.Teilnahmestatus != "Unbekannt" && model.Teilnahmestatus != "Abwesend")
                    {
                        Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Bitte sorge ggfs. für Ersatz! :)");
                        await dialog.ShowAsync();
                    }
                }
                var newAuftritt = await service.GetAuftrittAsync(model.Termin.Id);

                var modelNew = new AuftrittsterminViewModel();
                modelNew.RefreshFrom(newAuftritt, App.__spieler);

                var index = Model.Auftritte.IndexOf(model);
                Model.Auftritte.RemoveAt(index);
                Model.Auftritte.Insert(index, modelNew);
                modelNew.DoneLoading();
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
