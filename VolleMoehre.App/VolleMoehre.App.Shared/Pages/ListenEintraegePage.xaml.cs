using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using System.Threading.Tasks;
using VolleMoehre.App.Services;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ListenEintraegePage : Page
    {
        ListeViewModel _vm;
        ListenTyp _listentyp;
        IDialogService _dialogService;

        public ListenEintraegePage()
        {
            this.InitializeComponent();
            _dialogService = new DialogService();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _listentyp = (Contracts.Model.ListenTyp)e.Parameter;

            await InitAsync();
        }

        private async Task InitAsync(bool forceRefresh = false)
        {
            this.DataContext = _vm = new ListeViewModel();
            await _vm.InitBaseAsync();

            IListenService listenService = new ListenService(App.__APIKey);
            var liste = await listenService.GetListeAsync(_listentyp, forceRefresh);
            liste = liste.OrderBy(l => l.Name.ToLower()).ToList();
            foreach (var entry in liste)
                _vm.ListenEintraege.Add(entry);

            _vm.DoneLoading();
        }

        private async void EintragSelected(object sender, ItemClickEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            Liste ort = e.ClickedItem as Liste;
            this.Frame.Navigate(typeof(ListenEintragEditPage), ort);
        }

        private async void ListenEintragAnlegenClick(object sender, RoutedEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            this.Frame.Navigate(typeof(ListenEintragEditPage), new Liste() { Typ = _listentyp });
        }

        private async void ListenEintragAktualisierenClick(object sender, RoutedEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            await InitAsync(true);
        }

        private async void ListenEintragZufallseintragClick(object sender, RoutedEventArgs e)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var randomEntry = _vm.ListenEintraege[rand.Next(0, _vm.ListenEintraege.Count)];

            await _dialogService.ShowInfoMessageAsync(randomEntry.Name, "Voilá:");
        }
    }
}
