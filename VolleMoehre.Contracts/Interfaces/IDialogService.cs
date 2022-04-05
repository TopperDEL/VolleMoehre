using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface IDialogService
    {
        Task<bool> AskYesOrNoAsync(string message, string title);
        Task ShowErrorMessageAsync(string message, string title);
        Task ShowInfoMessageAsync(string message, string title);
    }
}
