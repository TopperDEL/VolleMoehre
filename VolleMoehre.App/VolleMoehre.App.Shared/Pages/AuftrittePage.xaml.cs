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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using VolleMoehre.Shared.Services;
using Plugin.Connectivity;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AuftrittePage : Page
    {
        AuftritteViewModel _vm;

        public AuftrittePage()
        {
            this.InitializeComponent();
            this.Loaded += AuftrittePage_Loaded;
        }

        private async void AuftrittePage_Loaded(object sender, RoutedEventArgs e)
        {
            await InitAsync();
        }

        private async Task InitAsync()
        {
            this.DataContext = _vm = new AuftritteViewModel();
            await _vm.InitBaseAsync();

            IAuftritteService auftritteSrv = new AuftritteService(App.__APIKey);
            try
            {
                var auftritte = (await auftritteSrv.GetAlleAuftritteAsync()).ToList();
                foreach (var auftritt in auftritte)
                    _vm.Auftritte.Add(AuftrittsterminViewModel.FromSingle(auftritt, App.__spieler));
            }
            catch (Exception ex)
            {

            }
            
            _vm.DoneLoading();
        }

        private async void AuftrittSelected(object sender, ItemClickEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            _vm.StartLoading();

            AuftrittsterminViewModel vm = e.ClickedItem as AuftrittsterminViewModel;
            this.Frame.Navigate(typeof(AuftrittEditPage), vm.Termin);
        }

        private async void AuftrittAnlegenClick(object sender, RoutedEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            _vm.StartLoading();

            this.Frame.Navigate(typeof(AuftrittEditPage), new Auftrittstermin() { Datum = DateTime.Now });
        }
    }
}
