using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using VolleMoehre.Shared.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class APIConnectionPage : Page
    {
        public ViewModels.APIConnectionPageViewModel _vm;

        public APIConnectionPage()
        {
            this.InitializeComponent();

            this.DataContext = _vm = new ViewModels.APIConnectionPageViewModel();
            _vm.Loading = false;
        }

        private async void ConnectClick(object sender, RoutedEventArgs e)
        {
            _vm.Loading = true;
            var connService = new APIConnectionService();

            var player = await connService.GetPlayerForAPIKey(_vm.SecretKey);

            _vm.Loading = false;
            if (player != null)
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values["APIKey"] = App.__APIKey = _vm.SecretKey;
                Frame.Navigate(typeof(InternMainPage));
            }
            else
            {
                _vm.ShowErrorMessage = true;
                await Task.Delay(2000);
                Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
