using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Interfaces
{
    public enum MailTypes
    {
        AnstehendeTermine,
        Auftritt_ModerationFehlt,
        Auftritt_MusikerFehlt,
        Auftritt_ModerationNachbereiten,
        Auftritt_OhneAussage,
        Auftritt_PresseInfo,
        Auftritt_StehtBaldAn,
        Auftritt_Nachbereiten,
        Auftritt_NichtOeffentlichErinnern,
        Training_OhneAussage,
        Training_StehtBaldAn,
        Training_LeiterFehlt,
        ResetPassword
    }
    public interface IMailTextGenerator
    {
        string GetMailText(MailTypes mailType, object mailParamsModel);
    }
}
