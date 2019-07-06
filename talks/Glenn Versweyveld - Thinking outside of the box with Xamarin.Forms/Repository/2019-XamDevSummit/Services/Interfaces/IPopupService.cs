using Prism.Commands;
using XamDevSummit.Controls;

namespace XamDevSummit.Services.Interfaces
{
    public interface IPopupService
    {
        void DisplayPopup(string title, string content, DelegateCommand<PopupResultEventArgs> command = null);
        bool Dismiss();
    }
}