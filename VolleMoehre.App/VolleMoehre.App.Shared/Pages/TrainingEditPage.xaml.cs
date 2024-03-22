using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VolleMoehre.Contracts.Model;
using VolleMoehre.App.Shared.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using VolleMoehre.Shared.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrainingEditPage : Page
    {
        Trainingstermin _edit;

        public TrainingEditPage()
        {
            this.InitializeComponent();

            Orte.ItemsSource = AuftrittsterminViewModel.AlleOrte;
            var _enumval = Enum.GetValues(typeof(Trainingstypen)).Cast<Trainingstypen>();
            TrainingstypenCombo.ItemsSource = _enumval.ToList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.DataContext = _edit = e.Parameter as Trainingstermin;
            TrainingstypenCombo.SelectedItem = _edit.Trainingstyp;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            var trainingsService = new TrainingsService(App.__APIKey);
            var success = await trainingsService.SaveTrainingAsync(_edit);
            if (success.Erfolgreich)
                this.Frame.GoBack();
            else
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(success.Fehlermeldung);
                await dialog.ShowAsync();
            }
            SaveButton.IsEnabled = true;
            CancelButton.IsEnabled = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = false;
            this.Frame.GoBack();
        }
    }
}
