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
using Windows.Storage;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VolleMoehre.App.Shared
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class InternMainPage : Page
    {
        public InternMainPage()
        {
            this.InitializeComponent();

            //Uno.UI.FeatureConfiguration.NativeListViewBase.RemoveItemAnimator = false;

            NavView.SelectedItem = NavView.MenuItems[0];
            ContentFrame.Navigate(typeof(AuftrittePage), null, new EntranceNavigationTransitionInfo());
            NavView.Header = "Auftritte";
        }

        private void NavigationInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string navItemTag = args.InvokedItem.ToString();
            switch (navItemTag)
            {
                case "Auftritte":
                    NavView.Header = "Auftritte";
                    ContentFrame.Navigate(typeof(AuftrittePage), null, new EntranceNavigationTransitionInfo());
                    break;
                case "Trainings":
                    NavView.Header = "Trainings";
                    ContentFrame.Navigate(typeof(TrainingsPage), null, new EntranceNavigationTransitionInfo());
                    break;
                case "Nachbereitung":
                    NavView.Header = "Nachbereitung";
                    ContentFrame.Navigate(typeof(NachbereitungPage), null, new EntranceNavigationTransitionInfo());
                    break;
                case "Orte":
                    NavView.Header = "Orte";
                    ContentFrame.Navigate(typeof(OrtePage), null, new EntranceNavigationTransitionInfo());
                    break;
                case "Spiele":
                    NavView.Header = "Spiele";
                    ContentFrame.Navigate(typeof(SpielePage), null, new EntranceNavigationTransitionInfo());
                    break;
                case "Abrechnung":
                    NavView.Header = "Abrechnung";
                    ContentFrame.Navigate(typeof(AbrechnungPage), null, new EntranceNavigationTransitionInfo());
                    break;
                case "Abfragen":
                    NavView.Header = "Abfragen";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.Abfragen, new EntranceNavigationTransitionInfo());
                    break;
                case "Beziehungen":
                    NavView.Header = "Beziehungen";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.Beziehungen, new EntranceNavigationTransitionInfo());
                    break;
                case "Charaktere":
                    NavView.Header = "Charaktere";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.Charaktere, new EntranceNavigationTransitionInfo());
                    break;
                case "Forderungen Match":
                    NavView.Header = "Forderungsliste Matches";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.ForderungenMatch, new EntranceNavigationTransitionInfo());
                    break;
                case "Gefühle":
                    NavView.Header = "Gefühle";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.Gefuehle, new EntranceNavigationTransitionInfo());
                    break;
                case "Genres":
                    NavView.Header = "Genres";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.Genres, new EntranceNavigationTransitionInfo());
                    break;
                case "Märchen":
                    NavView.Header = "Märchen";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.Maerchen, new EntranceNavigationTransitionInfo());
                    break;
                case "Möhre Kontaktdaten":
                    NavView.Header = "Möhre Kontaktdaten";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.MoehreKontaktdaten, new EntranceNavigationTransitionInfo());
                    break;
                case "Musikstile":
                    NavView.Header = "Musikstile";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.Musikstile, new EntranceNavigationTransitionInfo());
                    break;
                case "Sonstige Kontaktdaten":
                    NavView.Header = "Sonstige Kontaktdaten";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.SonstigeKontaktdaten, new EntranceNavigationTransitionInfo());
                    break;
                case "Spieleliste Improkabarett":
                    NavView.Header = "Spieleliste Improkabarett";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.SpielelisteImprokabarett, new EntranceNavigationTransitionInfo());
                    break;
                case "Spieleliste Kinderimpro":
                    NavView.Header = "Spieleliste Kinderimpro";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.SpielelisteKinderimpro, new EntranceNavigationTransitionInfo());
                    break;
                case "Spieleliste Klassiker":
                    NavView.Header = "Spieleliste Klassiker";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.SpielelisteKlassiker, new EntranceNavigationTransitionInfo());
                    break;
                case "Spieleliste Match":
                    NavView.Header = "Spieleliste Match";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.SpielelisteMatch, new EntranceNavigationTransitionInfo());
                    break;
                case "Spieleliste OpenAir":
                    NavView.Header = "Spieleliste OpenAir";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.SpielelisteOpenAir, new EntranceNavigationTransitionInfo());
                    break;
                case "Ticks":
                    NavView.Header = "Ticks";
                    ContentFrame.Navigate(typeof(ListenEintraegePage), Contracts.Model.ListenTyp.Ticks, new EntranceNavigationTransitionInfo());
                    break;
                case "Ausloggen":
                    DoLogout();
                    break;
            }
        }

        private void DoLogout()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values.Remove("APIKey");
            Frame.Navigate(typeof(MainPage));
        }
    }
}
