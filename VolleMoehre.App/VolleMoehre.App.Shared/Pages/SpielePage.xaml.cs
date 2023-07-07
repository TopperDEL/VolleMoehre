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
    public sealed partial class SpielePage : Page
    {
        SpieleViewModel _vm;

        public SpielePage()
        {
            this.InitializeComponent();

            this.Loaded += SpielePage_Loaded;
        }

        private async void SpielePage_Loaded(object sender, RoutedEventArgs e)
        {
            await InitAsync();
        }

        private async Task InitAsync()
        {
            this.DataContext = _vm = new SpieleViewModel();
            await _vm.InitBaseAsync();

            ISpieleService orteSrv = new SpieleService(App.__APIKey);
            var spiele = (await orteSrv.GetSpieleAsync()).ToList();
            spiele = spiele.OrderBy(s => s.Name).ToList(); ;
            foreach (var spiel in spiele)
                _vm.Spiele.Add(spiel);

            _vm.DoneLoading();
        }

        private async void SpielSelected(object sender, ItemClickEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Microsoft.UI.Popups.MessageDialog dialog = new Microsoft.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            Spiel spiel = e.ClickedItem as Spiel;
            this.Frame.Navigate(typeof(SpielEditPage), spiel);
        }

        private async void SpielAnlegenClick(object sender, RoutedEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Microsoft.UI.Popups.MessageDialog dialog = new Microsoft.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            this.Frame.Navigate(typeof(SpielEditPage), new Spiel());
        }
    }
}
