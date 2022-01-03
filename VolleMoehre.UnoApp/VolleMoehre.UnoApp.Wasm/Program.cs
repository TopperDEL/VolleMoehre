using System;
using Windows.UI.Xaml;

namespace VolleMoehre.UnoApp.Wasm
{
    public class Program
    {
        private static VolleMoehre.App.App _app;

        static int Main(string[] args)
        {
            Windows.UI.Xaml.Application.Start(_ => _app = new VolleMoehre.App.App());

            return 0;
        }
    }
}
