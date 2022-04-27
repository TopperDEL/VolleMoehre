using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using VolleMoehre.Contracts.Interfaces;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.Adapter.Calender
{
    public class CalenderExportService : ICalenderExporter
    {
        public byte[] TransferToiCal(Auftrittstermin auftrittsTermin)
        {
            throw new NotImplementedException();
        }

        public byte[] TransferToiCal(Trainingstermin trainingsTermin)
        {
            throw new NotImplementedException();
        }

        public string TransferToiCalFeed(Spieler spieler, List<Trainingstermin> trainingsTermine, List<Auftrittstermin> auftrittsTermine, List<Ort> orte)
        {
            Calendar iCalender = new Calendar();
            iCalender.Method = "PUBLISH";

            foreach (Auftrittstermin auftritt in auftrittsTermine.Where(a=>a.Helfer.Contains(spieler.Id) ||
                                                                           a.Moderator.Contains(spieler.Id) ||
                                                                           a.Spieler.Contains(spieler.Id) ||
                                                                           a.Vorgemerkt.Contains(spieler.Id)))
            {
                CalendarEvent moehreEvent = new CalendarEvent();
                moehreEvent.Start = new CalDateTime(auftritt.Datum);
                moehreEvent.Duration = TimeSpan.FromHours(2);
                if (auftritt.Vorgemerkt.Contains(spieler.Id))
                {
                    moehreEvent.Description = "Vorgemerkt für Volle Möhre: " + auftritt.Showtyp;
                }
                else
                {
                    moehreEvent.Description = "Volle Möhre: " + auftritt.Showtyp;
                }
                if(!string.IsNullOrEmpty(auftritt.OrtId) && orte.Where(o=>o.Id == auftritt.OrtId).Count() == 1)
                {
                    var beschreibung = orte.Where(o => o.Id == auftritt.OrtId).First().Bezeichnung;
                    moehreEvent.Location = beschreibung + " - Freitext: " + auftritt.FreitextInfoIntern;
                    if (!string.IsNullOrEmpty(beschreibung) && !beschreibung.Contains("Freitext"))
                    {
                        moehreEvent.Description = moehreEvent.Description + ", " + beschreibung;
                    }
                    else if(!string.IsNullOrEmpty(auftritt.SpezialOrtText))
                    {
                        moehreEvent.Description = moehreEvent.Description + ", " + auftritt.SpezialOrtText;
                    }
                    else
                    {
                        moehreEvent.Description = moehreEvent.Description + ", " + auftritt.FreitextInfoIntern;
                    }
                }
                if(!string.IsNullOrEmpty(auftritt.Ansprechpartner))
                {
                    moehreEvent.Description = moehreEvent.Description + ", " + auftritt.Ansprechpartner;
                }
                moehreEvent.Summary = moehreEvent.Description;
                iCalender.Events.Add(moehreEvent);
            }
            foreach (Trainingstermin training in trainingsTermine.Where(a => a.Teilnehmer.Contains(spieler.Id) ||
                                                                             a.Vorgemerkt.Contains(spieler.Id) ||
                                                                             a.Leiter.Contains(spieler.Id) ||
                                                                             a.Online.Contains(spieler.Id)))
            {
                CalendarEvent moehreEvent = new CalendarEvent();
                moehreEvent.Start = new CalDateTime(training.Datum);
                if (training.Dauer == 0)
                {
                    moehreEvent.Duration = TimeSpan.FromHours(2);
                }
                else
                {
                    moehreEvent.Duration = TimeSpan.FromMinutes(training.Dauer);
                }
                if (training.Vorgemerkt.Contains(spieler.Id))
                {
                    moehreEvent.Description = "Vorgemerkt für Volle Möhre: " + training.FreitextInfo;
                }
                else
                {
                    moehreEvent.Description = "Volle Möhre: " + training.FreitextInfo;
                }
                if (!string.IsNullOrEmpty(training.OrtId) && orte.Where(o => o.Id == training.OrtId).Count() == 1)
                {
                    var beschreibung = orte.Where(o => o.Id == training.OrtId).First().Bezeichnung;
                    moehreEvent.Location = beschreibung + " - Freitext: " + training.FreitextInfo;
                    if (!string.IsNullOrEmpty(beschreibung) && !beschreibung.Contains("Freitext"))
                    {
                        moehreEvent.Description = moehreEvent.Description + ", " + beschreibung;
                    }
                }
                moehreEvent.Summary = moehreEvent.Description;
                iCalender.Events.Add(moehreEvent);
            }
            iCalender.AddProperty("CALSCALE", "GREGORIAN");

            CalendarSerializer serializer = new CalendarSerializer();
            return serializer.SerializeToString(iCalender);
        }

        public byte[] TransferToiCalPublic(Auftrittstermin auftrittsTermin)
        {
            throw new NotImplementedException();
        }
    }
}
