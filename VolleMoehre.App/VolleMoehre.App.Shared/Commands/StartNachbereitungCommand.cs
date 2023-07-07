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
    public class StartNachbereitungCommand : ICommand
    {
        private AuftrittsterminViewModel Model;

        public event EventHandler CanExecuteChanged;

        public StartNachbereitungCommand(AuftrittsterminViewModel model)
        {
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
                Microsoft.UI.Popups.MessageDialog dialog = new Microsoft.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            int gefahreneKilometer = 0;

            foreach (var auslage in Model.Termin.Auslagen)
            {
                if (auslage.SpielerId == App.__spieler.Id && auslage.GefahreneKilometer > 0)
                    gefahreneKilometer = auslage.GefahreneKilometer;
            }

            SpielerStatus spielerStatus = SpielerStatus.Abwesend;
            if (Model.Termin.Abwesend.Contains(App.__spieler.Id))
                spielerStatus = SpielerStatus.Abwesend;
            else if (Model.Termin.Helfer.Contains(App.__spieler.Id))
                spielerStatus = SpielerStatus.Helfer;
            else if (Model.Termin.Moderator.Contains(App.__spieler.Id))
                spielerStatus = SpielerStatus.Moderator;
            else if (Model.Termin.Spieler.Contains(App.__spieler.Id))
                spielerStatus = SpielerStatus.Spieler;
            else if (Model.Termin.Vorgemerkt.Contains(App.__spieler.Id))
                spielerStatus = SpielerStatus.Vorgemerkt;

            var frame = (Microsoft.UI.Xaml.Controls.Frame)Microsoft.UI.Xaml.Window.Current.Content;
            frame.Navigate(typeof(NachbereitungEditPage), new NacharbeitungsErgebnis() { Id = Model.Termin.Id, SpielerId = App.__spieler.Id, SpielerStatus = spielerStatus, GefahreneKilometer = gefahreneKilometer });
        }
    }
}
