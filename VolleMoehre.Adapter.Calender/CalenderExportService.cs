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

        public string TransferToiCalFeed(Spieler spieler, List<Trainingstermin> trainingsTermine, List<Auftrittstermin> auftrittsTermine)
        {
            byte[] retBytes;

            //Alarm alarm = new Alarm();
            //alarm.Action =  AlarmAction.Display;
            //alarm.Description = "Volle-Möhre Kalender für " + spieler.Name;
            //alarm.Trigger = new Trigger();
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
                //moehreEvent.Location = auftritt.Ort.Bezeichnung + " - Freitext: " + auftritt.FreitextInfoIntern;
                moehreEvent.Summary = moehreEvent.Description;
                //moehreEvent.Url = new Uri("http://www.vollemoehre.de/Auftritte/IndexIntern/#" + auftritt.TerminID);
                //moehreEvent.Alarms.Add(alarm);
                iCalender.Events.Add(moehreEvent);
            }
            //foreach (Trainingstermin auftritt in auftritteSpielerVGM)
            //{
            //    auftritt.OrtReference.Load();
            //    DDay.iCal.Event moehreEvent = new DDay.iCal.Event();
            //    moehreEvent.DTStart = new DDay.iCal.iCalDateTime(auftritt.Datum);
            //    moehreEvent.Duration = TimeSpan.FromHours(2);
            //    moehreEvent.Description = "VGM Volle Möhre: " + auftritt.Showtyp;
            //    moehreEvent.Location = auftritt.Ort.Bezeichnung + " - Freitext: " + auftritt.FreitextInfo;
            //    moehreEvent.Summary = "Vorgemerkt für Volle Möhre: " + auftritt.Showtyp;
            //    moehreEvent.Url = new Uri("http://www.vollemoehre.de/Auftritte/" + auftritt.TerminID);
            //    moehreEvent.Alarms.Add(alarm);
            //    iCalender.Events.Add(moehreEvent);
            //}
            //foreach (Training training in trainingsSpielerVGM)
            //{
            //    training.OrtReference.Load();
            //    DDay.iCal.Event moehreEvent = new DDay.iCal.Event();
            //    moehreEvent.DTStart = new DDay.iCal.iCalDateTime(training.Datum);
            //    moehreEvent.Duration = TimeSpan.FromHours(2);
            //    moehreEvent.Description = "VGM Volle Möhre: " + training.FreitextInfo;
            //    moehreEvent.Location = training.Ort.Bezeichnung + " - Freitext: " + training.FreitextInfo;
            //    moehreEvent.Summary = "Vorgemerkt für Volle Möhre: " + training.FreitextInfo;
            //    moehreEvent.Url = new Uri("http://www.vollemoehre.de/Trainings/" + training.TrainingID);
            //    moehreEvent.Alarms.Add(alarm);
            //    iCalender.Events.Add(moehreEvent);
            //}
            foreach (Trainingstermin training in trainingsTermine.Where(a => a.Teilnehmer.Contains(spieler.Id) ||
                                                                             a.Vorgemerkt.Contains(spieler.Id)))
            {
                CalendarEvent moehreEvent = new CalendarEvent();
                moehreEvent.Start = new CalDateTime(training.Datum);
                moehreEvent.Duration = TimeSpan.FromHours(2);
                if (training.Vorgemerkt.Contains(spieler.Id))
                {
                    moehreEvent.Description = "Vorgemerkt für Volle Möhre: " + training.FreitextInfo;
                }
                else
                {
                    moehreEvent.Description = "Volle Möhre: " + training.FreitextInfo;
                }                
                //moehreEvent.Location = training.Ort.Bezeichnung + " - Freitext: " + training.FreitextInfo;
                moehreEvent.Summary = moehreEvent.Description;
                //moehreEvent.Url = new Uri("http://www.vollemoehre.de/Trainings/" + training.TrainingID);
                //moehreEvent.Alarms.Add(alarm);
                iCalender.Events.Add(moehreEvent);
            }
            iCalender.AddProperty("CALSCALE", "GREGORIAN");

            CalendarSerializer serializer = new CalendarSerializer();
            return serializer.SerializeToString(iCalender);

            //using (System.IO.MemoryStream mstream = new System.IO.MemoryStream())
            //{
            //    CalendarSerializer serializer = new CalendarSerializer();
            //    serializer.SerializeToString(iCalender);
            //    System.Text.UTF8Encoding utf8OhneBOM = new System.Text.UTF8Encoding(false);
            //    serializer.Serialize(iCalender, mstream, utf8OhneBOM);
            //    mstream.Flush();
            //    retBytes = mstream.GetBuffer();
            //}

            //return retBytes;
        }

        public byte[] TransferToiCalPublic(Auftrittstermin auftrittsTermin)
        {
            throw new NotImplementedException();
        }
    }
}
