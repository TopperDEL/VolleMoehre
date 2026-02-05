using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.App.Shared.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool Loading { get; set; }
        public bool LoadingFinished { get; set; }

        public BaseViewModel()
        {
            StartLoading();
        }

        public async Task InitBaseAsync()
        {
            await App.InitSpielerAsync();
        }

        public void StartLoading()
        {
            Loading = true;
            LoadingFinished = false;
        }

        public void DoneLoading()
        {
            Loading = false;
            LoadingFinished = true;
        }

        public void DoneLoading(string changedPropertyName)
        {
            DoneLoading();
            RaiseChanged(changedPropertyName);
        }

        protected void RaiseChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
