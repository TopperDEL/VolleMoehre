using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VolleMoehre.App.Shared.ViewModels
{
    [Bindable(BindableSupport.Yes)]
    public class APIConnectionPageViewModel:BaseViewModel
    {
        public string SecretKey { get; set; }
        public bool ShowErrorMessage { get; set; }
    }
}
