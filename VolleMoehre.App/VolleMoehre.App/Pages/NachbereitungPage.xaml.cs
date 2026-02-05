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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NachbereitungPage : Page
    {
        NachbereitungenViewModel _vm;

        public NachbereitungPage()
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
            this.DataContext = _vm = new NachbereitungenViewModel();
            await _vm.InitBaseAsync();

            INachbereitungService nachbereitungSrv = new NachbereitungService(App.__APIKey);
            var nachbereitungen = (await nachbereitungSrv.GetToDoAsync()).ToList();
            foreach (var nachbereitung in nachbereitungen)
                _vm.ToDos.Add(AuftrittsterminViewModel.FromSingle(nachbereitung, App.__spieler));

            _vm.DoneLoading();
        }
    }
}
