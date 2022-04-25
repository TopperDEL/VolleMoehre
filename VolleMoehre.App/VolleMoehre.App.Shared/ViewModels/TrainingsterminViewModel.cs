using System;
using System.Collections.Generic;
using System.Text;
using VolleMoehre.Contracts.Model;
using System.Linq;
using VolleMoehre.App.Shared.Commands;

namespace VolleMoehre.App.Shared.ViewModels
{
    public class TrainingsterminViewModel : BaseViewModel
    {
        public TrainingsterminViewModel()
        {
            CanTeilnehmer = CanLeiter = CanVormerken = CanAbwesend = CanOnline = true;
        }

        public void RefreshFrom(Trainingstermin termin, Spieler spieler)
        {
            try
            {
                Termin = termin;
                Id = termin.Id;
                Trainingstyp = termin.Trainingstyp;
                Datum = String.Format("{0:ddd}", termin.Datum) + ", " + String.Format("{0:g}", termin.Datum);
                if (AuftrittsterminViewModel.AlleOrte != null && AuftrittsterminViewModel.AlleOrte.Count > 0)
                {
                    var ort = AuftrittsterminViewModel.AlleOrte.Where(o => o.Id == termin.OrtId).First();
                    Ort = ort != null ? ort.Bezeichnung : "Unbekannter Ort";
                }
                FreitextInfo = termin.FreitextInfo;
                CanTeilnehmer = CanLeiter = CanVormerken = CanAbwesend = true;
                AussageNoetig = false;
                if (termin.Teilnehmer.Contains(spieler.Id))
                {
                    Teilnahmestatus = "Teilnehmer";
                    TeilnahmestatusFarbe = "Green";
                    CanTeilnehmer = false;
                }
                else if (termin.Leiter.Contains(spieler.Id))
                {
                    Teilnahmestatus = "Leiter";
                    TeilnahmestatusFarbe = "Green";
                    CanLeiter = false;
                }
                else if (termin.Vorgemerkt.Contains(spieler.Id))
                {
                    Teilnahmestatus = "Vorgemerkt";
                    TeilnahmestatusFarbe = "Orange";
                    CanVormerken = false;
                }
                else if (termin.Online.Contains(spieler.Id))
                {
                    Teilnahmestatus = "Nur Online";
                    TeilnahmestatusFarbe = "Blue";
                    CanOnline = false;
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
                Leiter = GetSpielerListe(termin.Leiter);
                LeiterFehlt = termin.Leiter.Count == 0;
                Teilnehmer = GetSpielerListe(termin.Teilnehmer);
                TeilnehmerLeer = termin.Teilnehmer.Count == 0;
                Vorgemerkte = GetSpielerListe(termin.Vorgemerkt);
                VorgemerkteLeer = termin.Vorgemerkt.Count == 0;
                Online = GetSpielerListe(termin.Online);
                OnlineLeer = termin.Online.Count == 0;
                Abwesende = GetSpielerListe(termin.Abwesend);
                AbwesendeLeer = termin.Abwesend.Count == 0;
               
                Kommentare = new List<KommentarViewModel>();
                foreach (var kommentar in termin.Kommentare)
                    Kommentare.Add(new KommentarViewModel() { Spieler = GetSpielerName(kommentar.SpielerId) + ": ", Kommentar = "\"" + kommentar.Text + "\"" });

            }
            catch
            {

            }
        }

        

        public static TrainingsterminViewModel FromSingle(Trainingstermin termin, Spieler spieler)
        {
            var tvm = new TrainingsterminViewModel();
            tvm.RefreshFrom(termin, spieler);
            tvm.DoneLoading();

            return tvm;
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
            return AuftrittsterminViewModel.AlleSpieler.Where(s => s.Id == spielerId).First().Name;
        }

        public string Id { get; set; }
        public string Datum { get; set; }
        public Trainingstypen Trainingstyp { get; set; }
        public string Ort { get; set; }
        public string FreitextInfo { get; set; }
        public string Teilnahmestatus { get; set; }
        public string TeilnahmestatusFarbe { get; set; }
        public string Leiter { get; set; }
        public bool LeiterFehlt { get; set; }
        public string Teilnehmer { get; set; }
        public bool TeilnehmerLeer { get; set; }
        public string Vorgemerkte { get; set; }
        public bool VorgemerkteLeer { get; set; }
        public string Online { get; set; }
        public bool OnlineLeer { get; set; }
        public string Abwesende { get; set; }
        public bool AbwesendeLeer { get; set; }
        public List<KommentarViewModel> Kommentare { get; set; }
        public bool CanLeiter { get; set; }
        public bool CanTeilnehmer { get; set; }
        public bool CanVormerken { get; set; }
        public bool CanOnline { get; set; }
        public bool CanAbwesend { get; set; }
        public bool AussageNoetig { get; set; }

        public Trainingstermin Termin { get; set; }
    }
}
