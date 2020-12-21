using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel _vm;

        public MainPage()
        {
            this.InitializeComponent();

            InitAsync();
        }

        private async Task InitAsync()
        {
            IAuftritteService auftritteSrv = new AuftritteService(App.__APIKey);
            this.DataContext = _vm = new MainPageViewModel();
            var auftritte = (await auftritteSrv.GetOeffentlicheAuftritteAsync()).ToList();
            foreach (var auftritt in auftritte)
                _vm.OeffentlicheAuftritte.Add(auftritt);

            _vm.DoneLoading("OeffentlicheAuftritte");
        }

        private void LogoTapped(object sender, TappedRoutedEventArgs e)
        {
            LogoActivated();
        }

        private void LogoClicked(object sender, RoutedEventArgs e)
        {
            LogoActivated();
        }

        private void LogoActivated()
        {
            _vm.LogoTapCount++;

            if (_vm.LogoTapCount >= 7)
            {
                _vm.LogoTapCount = 0;
                Frame.Navigate(typeof(APIConnectionPage));
            }
        }
    }
}
