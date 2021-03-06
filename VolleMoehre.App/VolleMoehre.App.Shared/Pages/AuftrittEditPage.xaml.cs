﻿using System;
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

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AuftrittEditPage : Page
    {
        Auftrittstermin _edit;

        public AuftrittEditPage()
        {
            this.InitializeComponent();

            Orte.ItemsSource = AuftrittsterminViewModel.AlleOrte;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.DataContext = _edit = e.Parameter as Auftrittstermin;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            var auftritteService = new AuftritteService(App.__APIKey);
            var success = await auftritteService.SaveAuftrittAsync(_edit);
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var auftritteService = new AuftritteService(App.__APIKey);
            auftritteService.DeleteAuftrittAsync(_edit);
            SaveButton.IsEnabled = false;
            this.Frame.GoBack();
        }
    }
}
