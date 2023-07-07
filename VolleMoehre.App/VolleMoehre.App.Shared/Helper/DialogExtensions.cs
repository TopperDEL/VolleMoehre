﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VolleMoehre.App.Services;
using Windows.Foundation;
using Microsoft.UI.Xaml.Controls;

namespace VolleMoehre.App.Helper
{
    internal static class DialogExtensions
    {
        internal static async Task<ContentDialogResult> ShowOneAtATimeAsync(this ContentDialog dialog, TimeSpan? timeout = null, CancellationToken? token = null)
        {
            return await DialogService.OneAtATimeAsync(async () => await dialog.ShowAsync(), timeout, token);
        }

        internal static ContentDialog SetPrimaryButton(this ContentDialog dialog, string text)
        {
            dialog.PrimaryButtonText = text;
            dialog.IsPrimaryButtonEnabled = true;
            return dialog;
        }

        internal static ContentDialog SetCloseButton(this ContentDialog dialog, string text)
        {
            dialog.CloseButtonText = text;
            return dialog;
        }

        internal static ContentDialog SetPrimaryButton(this ContentDialog dialog, string text,
            TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs> clickHandler)
        {
            dialog.SetPrimaryButton(text);
            dialog.PrimaryButtonClick += clickHandler;
            return dialog;
        }

        internal static ContentDialog SetSecondaryButton(this ContentDialog dialog, string text)
        {
            dialog.SecondaryButtonText = text;
            dialog.IsSecondaryButtonEnabled = true;
            return dialog;
        }

        internal static ContentDialog SetSecondaryButton(this ContentDialog dialog, string text,
            TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs> clickHandler)
        {
            dialog.SetSecondaryButton(text);
            dialog.SecondaryButtonClick += clickHandler;
            return dialog;
        }
    }
}