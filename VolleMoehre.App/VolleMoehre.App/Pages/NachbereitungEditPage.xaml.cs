using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.Contracts.Model;
using VolleMoehre.Shared.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class NachbereitungEditPage : Page
    {
        NacharbeitungsErgebnis _edit;

        public NachbereitungEditPage()
        {
            this.InitializeComponent();

            var _enumval = Enum.GetValues(typeof(SpielerStatus)).Cast<SpielerStatus>();
            _enumval = _enumval.Where(e => e != SpielerStatus.Leiter && e != SpielerStatus.Teilnehmer);
            SpielerStatusCombo.ItemsSource = _enumval.ToList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.DataContext = _edit = e.Parameter as NacharbeitungsErgebnis;
            SpielerStatusCombo.SelectedItem = _edit.SpielerStatus;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = false;
            var nachbereitungService = new NachbereitungService(App.__APIKey);
            var success = await nachbereitungService.SaveNachbereitungAsync(_edit);
            if (success)
                this.Frame.GoBack(); //TODO: Liste wird noch nicht aktualisiert
            else
            {
                //TODO
            }
            SaveButton.IsEnabled = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = false;
            this.Frame.GoBack();
        }
    }
}
