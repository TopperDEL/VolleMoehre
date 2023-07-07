using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.Contracts.Model;
using VolleMoehre.App.Shared.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using VolleMoehre.Shared.Services;
using Plugin.Connectivity;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TrainingsPage : Page
    {
        TrainingsViewModel _vm;

        public TrainingsPage()
        {
            this.InitializeComponent();
            this.Loaded += TrainingsPage_Loaded;
        }

        private async void TrainingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await InitAsync();
        }

        private async Task InitAsync()
        {
            this.DataContext = _vm = new TrainingsViewModel();
            await _vm.InitBaseAsync();

            ITrainingsService trainingsSrv = new TrainingsService(App.__APIKey);
            var trainings = (await trainingsSrv.GetTrainingsAsync()).ToList();
            foreach (var training in trainings)
                _vm.Trainings.Add(TrainingsterminViewModel.FromSingle(training, App.__spieler));

            _vm.DoneLoading();
        }

        private async void TrainingSelected(object sender, ItemClickEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Microsoft.UI.Popups.MessageDialog dialog = new Microsoft.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            TrainingsterminViewModel vm = e.ClickedItem as TrainingsterminViewModel;
            this.Frame.Navigate(typeof(TrainingEditPage), vm.Termin);
        }

        private async void TrainingAnlegenClick(object sender, RoutedEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Microsoft.UI.Popups.MessageDialog dialog = new Microsoft.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            this.Frame.Navigate(typeof(TrainingEditPage), new Trainingstermin() { Datum = DateTime.Now });
        }
    }
}
