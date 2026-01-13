using System;
using System.Collections.Generic;
using System.Text;
using VolleMoehre.Contracts.Model;
using System.Linq;
using VolleMoehre.App.Shared.Commands;
using System.Windows.Input;

namespace VolleMoehre.App.Shared.ViewModels
{
    public class AuftrittsterminViewModel: BaseViewModel
    {
        public static List<Spieler> AlleSpieler = new List<Spieler>();
        public static List<Ort> AlleOrte = new List<Ort>();
        public ICommand StartNachbereitungCommand;

        public AuftrittsterminViewModel()
        {
            CanSpieler = CanModeration = CanHelfer = CanVormerken = CanAbwesend = true;
            StartNachbereitungCommand = new StartNachbereitungCommand(this);
        }

        public void RefreshFrom(Auftrittstermin termin, Spieler spieler)
        {
            try
            {
                Termin = termin;
                Id = termin.Id;
                Showtyp = termin.Showtyp;
                Datum = String.Format("{0:ddd}", termin.Datum) + ", " + String.Format("{0:g}", termin.Datum);
                if (AlleOrte != null && AlleOrte.Count > 0)
                {
                    var ort = AlleOrte.Where(o => o.Id == termin.OrtId).FirstOrDefault();
                    Ort = ort != null ? ort.Bezeichnung : "Unbekannter Ort";
                }
                FreitextIntern = termin.FreitextInfoIntern;
                FreitextExtern = termin.FreitextInfoExtern;
                Oeffentlich = termin.Oeffentlich ? "Ja" : "Nein";
                CanSpieler = CanAbwesend = CanHelfer = CanModeration = CanVormerken = true;
                AussageNoetig = false;
                if (termin.Spieler.Contains(spieler.Id))
                {
                    Teilnahmestatus = "Spieler";
                    TeilnahmestatusFarbe = "Green";
                    CanSpieler = false;
                }
                else if (termin.Moderator.Contains(spieler.Id))
                {
                    Teilnahmestatus = "Moderator";
                    TeilnahmestatusFarbe = "Green";
                    CanModeration = false;
                }
                else if (termin.Helfer.Contains(spieler.Id))
                {
                    Teilnahmestatus = "Helfer";
                    TeilnahmestatusFarbe = "Green";
                    CanHelfer = false;
                }
                else if (termin.Vorgemerkt.Contains(spieler.Id))
                {
                    Teilnahmestatus = "Vorgemerkt";
                    TeilnahmestatusFarbe = "Orange";
                    CanVormerken = false;
                }
                else if (termin.Abwesend.Contains(spieler.Id))
                {
                    Teilnahmestatus = "Abwesend";
                    TeilnahmestatusFarbe = "Orange";
                    CanAbwesend = false;
                }
                else
                {
                    Teilnahmestatus = "Unbekannt";
                    TeilnahmestatusFarbe = "Red";
                    AussageNoetig = true;
                }
                Moderation = GetSpielerListe(termin.Moderator);
                ModerationFehlt = termin.Moderator.Count == 0;
                Spieler = GetSpielerListe(termin.Spieler);
                SpielerLeer = termin.Spieler.Count == 0;
                if (!string.IsNullOrEmpty(termin.Musiker))
                {
                    Musiker = termin.Musiker;
                    MusikerLeer = false;
                }
                else
                {
                    MusikerLeer = true;
                    Musiker = "";
                }
                Helfer = GetSpielerListe(termin.Helfer);
                HelferLeer = termin.Helfer.Count == 0;
                Vorgemerkte = GetSpielerListe(termin.Vorgemerkt);
                VorgemerkteLeer = termin.Vorgemerkt.Count == 0;
                Abwesende = GetSpielerListe(termin.Abwesend);
                AbwesendeLeer = termin.Abwesend.Count == 0;
                if (termin.BenoetigteSpieler <= termin.Moderator.Count + termin.Spieler.Count)
                {
                    BenoetigteSpieler = "Ausreichend";
                    BenoetigteSpielerVisible = false;
                }
                else
                {
                    BenoetigteSpieler = "Weitere " + (termin.BenoetigteSpieler - termin.Moderator.Count - termin.Spieler.Count) + " Spieler nötig!";
                    BenoetigteSpielerVisible = true;
                }

                NachbereitungNoetig = false;
                if (!termin.NachbereitetVon.Contains(spieler.Id))
                    NachbereitungNoetig = true;

                if (termin.Auslagen.Where(a => a.SpielerId == spieler.Id && !string.IsNullOrEmpty(a.ToString())).Count() > 0)
                {
                    var auslage = termin.Auslagen.Where(a => a.SpielerId == spieler.Id && !string.IsNullOrEmpty(a.ToString())).First();
                    Auslagen = auslage.ToString();
                }
                if(string.IsNullOrEmpty(Auslagen))
                    Auslagen = "Keine Auslagen angegeben";

                Kommentare = new List<KommentarViewModel>();
                foreach (var kommentar in termin.Kommentare)
                    Kommentare.Add(new KommentarViewModel() { Spieler = GetSpielerName(kommentar.SpielerId) + ": ", Kommentar = "\"" + kommentar.Text + "\"" });
            }
            catch
            {

            }
        }

        

        public static AuftrittsterminViewModel FromSingle(Auftrittstermin termin, Spieler spieler)
        {
            var avm = new AuftrittsterminViewModel();
            avm.RefreshFrom(termin, spieler);
            avm.DoneLoading();

            return avm;
        }

        private static string GetSpielerListe(List<string> spielerIds)
        {
            string ret = string.Empty;
            foreach (var spielerId in spielerIds)
                if (ret == string.Empty)
                    ret = GetSpielerName(spielerId);
                else
                    ret = ret + ", " + GetSpielerName(spielerId);
            return ret;
        }

        private static string GetSpielerName(string spielerId)
        {
            return AlleSpieler.Where(s => s.Id == spielerId).First().Name;
        }

        public string Id { get; set; }
        public string Showtyp { get; set; }
        public string Datum { get; set; }
        public string Ort { get; set; }
        public string FreitextIntern { get; set; }
        public string FreitextExtern { get; set; }
        public string Oeffentlich { get; set; }
        public string Teilnahmestatus { get; set; }
        public string TeilnahmestatusFarbe { get; set; }
        public string Moderation { get; set; }
        public bool ModerationFehlt { get; set; }
        public string Spieler { get; set; }
        public bool SpielerLeer { get; set; }
        public string Musiker { get; set; }
        public bool MusikerLeer { get; set; }
        public string Helfer { get; set; }
        public bool HelferLeer { get; set; }
        public string Vorgemerkte { get; set; }
        public bool VorgemerkteLeer { get; set; }
        public string Abwesende { get; set; }
        public bool AbwesendeLeer { get; set; }
        public string BenoetigteSpieler { get; set; }
        public bool BenoetigteSpielerVisible { get; set; }
        public List<KommentarViewModel> Kommentare { get; set; }
        public bool CanModeration { get; set; }
        public bool CanSpieler { get; set; }
        public bool CanHelfer { get; set; }
        public bool CanVormerken { get; set; }
        public bool CanAbwesend { get; set; }
        public bool AussageNoetig { get; set; }
        public bool NachbereitungNoetig { get; set; }
        public string Auslagen { get; set; }


        public Auftrittstermin Termin { get; set; }
    }
}
