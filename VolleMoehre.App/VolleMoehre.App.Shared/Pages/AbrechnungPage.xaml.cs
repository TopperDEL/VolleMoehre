using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VolleMoehre.Contracts.Interfaces;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AbrechnungPage : Page
    {
        AbrechnungViewModel _vm;

        public AbrechnungPage()
        {
            this.InitializeComponent();

            this.DataContext = _vm = new AbrechnungViewModel();
            _vm.DoneLoading();
        }

        private async void StarteAbrechnungClick(object sender, RoutedEventArgs e)
        {
            if (!ServiceBase.IsOnline())
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Du bist gerade leider nicht online. Bitte prüfe deine Verbindung.");
                await dialog.ShowAsync();
                return;
            }

            _vm.Loading = true;

            IAbrechnungsAPIService abrechnungSrv = new AbrechnungsService(App.__APIKey);
            var abrechnungen = (await abrechnungSrv.GetAbrechnungAsync(_vm.Beginn.Date,_vm.Ende.Date)).ToList();
            foreach (var abrechnung in abrechnungen)
                _vm.Abrechnungen.Add(abrechnung);

            _vm.DoneLoading();
        }
    }
}
