using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VolleMoehre.Contracts.Model;
using VolleMoehre.Shared.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListenEintragEditPage : Page
    {
        Liste _edit;

        public ListenEintragEditPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.DataContext = _edit = e.Parameter as Liste;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            var listenService = new ListenService(App.__APIKey);
            var success = await listenService.SaveListenEintragAsync(_edit);
            if (success.Erfolgreich)
                this.Frame.GoBack();
            else
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(success.Fehlermeldung);
                await dialog.ShowAsync();
                return;
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
