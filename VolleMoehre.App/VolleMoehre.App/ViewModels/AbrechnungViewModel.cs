using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VolleMoehre.Contracts.Model;

namespace VolleMoehre.App.Shared.ViewModels
{
    public class AbrechnungViewModel:BaseViewModel
    {
        public DateTimeOffset Beginn { get; set; }
        public DateTimeOffset Ende { get; set; }

        public ObservableCollection<Spielerabrechnung> Abrechnungen { get; set; }

        public AbrechnungViewModel()
        {
            Abrechnungen = new ObservableCollection<Spielerabrechnung>();
            if(2 <= DateTime.Now.Month && DateTime.Now.Month <= 7)//Zwischen Februar und Juli das erste Halbjahr
            {
                Beginn = DateTime.Parse("01.01." + DateTime.Now.Year);
                Ende = DateTime.Parse("30.06." + DateTime.Now.Year);
            }
            else //Ansonsten das zweite Halbjahr
            {
                Beginn = DateTime.Parse("01.01." + DateTime.Now.Year);
                Ende = DateTime.Parse("31.12." + DateTime.Now.Year);
            }
        }
    }
}
