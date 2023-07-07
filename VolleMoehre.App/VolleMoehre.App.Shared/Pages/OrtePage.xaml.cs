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
    public sealed partial class OrtePage : Page
    {
        OrteViewModel _vm;

        public OrtePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            InitAsync();
        }

        private async Task InitAsync()
        {
            this.DataContext = _vm = new OrteViewModel();
            await _vm.InitBaseAsync();

            IOrteService orteSrv = new OrteService(App.__APIKey);
            var orte = (await orteSrv.GetOrteAsync()).ToList();
            orte = orte.OrderBy(o => o.Bezeichnung).ToList();
            foreach (var ort in orte)
                _vm.Orte.Add(ort);

            _vm.DoneLoading();
        }

        private async void OrtSelected(object sender, ItemClickEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Microsoft.UI.Popups.MessageDialog dialog = new Microsoft.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            Ort ort = e.ClickedItem as Ort;
            this.Frame.Navigate(typeof(OrtEditPage), ort);
        }

        private async void OrtAnlegenClick(object sender, RoutedEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Microsoft.UI.Popups.MessageDialog dialog = new Microsoft.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            this.Frame.Navigate(typeof(OrtEditPage), new Ort());
        }
    }
}
