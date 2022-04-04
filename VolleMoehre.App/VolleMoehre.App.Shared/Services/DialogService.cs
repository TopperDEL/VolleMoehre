using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VolleMoehre.App.Helper;
using VolleMoehre.Contracts.Interfaces;
using Windows.UI.Xaml.Controls;

namespace VolleMoehre.App.Services
{
    public class DialogService : IDialogService
    {
        private static readonly SemaphoreSlim _oneAtATimeAsync = new SemaphoreSlim(1, 1);

        public async Task<bool> AskYesOrNoAsync(string message, string title)
        {
            bool result = false;

            var askDialog = new ContentDialog
            {
                Title = title,
                Content = message
            }.SetPrimaryButton("Ja", (d, e) => { result = true; }).SetSecondaryButton("Nein", (d, e) => { result = false; });

            await askDialog.ShowOneAtATimeAsync();

            return result;
        }

        internal static async Task<T> OneAtATimeAsync<T>(Func<Task<T>> show, TimeSpan? timeout, CancellationToken? token)
        {
            var to = timeout ?? TimeSpan.FromHours(1);
            var tk = token ?? new CancellationToken(false);
            if (!await _oneAtATimeAsync.WaitAsync(to, tk))
            {
                throw new Exception($"{nameof(DialogService)}.{nameof(OneAtATimeAsync)} has timed out.");
            }
            try
            {
                return await show();
            }
            finally
            {
                _oneAtATimeAsync.Release();
            }
        }

        public async Task ShowErrorMessageAsync(string message, string title)
        {
            try
            {
                var errorDialog = new ContentDialog
                {
                    Title = title,
                    Content = message
                }.SetPrimaryButton("Ok");

                await errorDialog.ShowOneAtATimeAsync();
            }
            catch { }
        }

        public async Task ShowInfoMessageAsync(string message, string title)
        {
            try
            {
                var infoDialog = new ContentDialog
                {
                    Title = title,
                    Content = message
                }.SetPrimaryButton("Schließen");

                await infoDialog.ShowOneAtATimeAsync();
            }
            catch { }
        }
    }
}