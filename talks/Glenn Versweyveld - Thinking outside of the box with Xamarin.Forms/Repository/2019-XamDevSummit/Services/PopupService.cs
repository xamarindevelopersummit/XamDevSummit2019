using System;
using Prism.Commands;
using Xamarin.Forms;
using XamDevSummit.Controls;
using XamDevSummit.Services.Interfaces;

namespace XamDevSummit.Services
{
    public class PopupService : IPopupService
    {
        private DelegateCommand<PopupResultEventArgs> _command;
        private SelectionPopup _popup;

        public bool Dismiss()
        {
            return RemovePopup();
        }

        public void DisplayPopup(string title, string content, DelegateCommand<PopupResultEventArgs> command = null)
        {
            _command = command;
            _popup = new SelectionPopup();

            _popup.Title = title;
            _popup.Message = content;

            _popup.Click += OnClicked;

            if (Application.Current.MainPage is MasterDetailPage current)
            {
                var cnt = ((ContentPage)((NavigationPage)current.Detail).CurrentPage).Content;
                if (cnt is Grid grd)
                {
                    grd.Children.Add(_popup, 0, 0);
                    Grid.SetRowSpan(_popup, 3);
                }
            }
        }

        private bool RemovePopup()
        {
            if (_popup != null && Application.Current.MainPage is MasterDetailPage current)
            {
                var cnt = ((ContentPage)((NavigationPage)current.Detail).CurrentPage).Content;
                if (cnt is Grid grd)
                    grd.Children.Remove(_popup);

                _popup.Click -= OnClicked;
                _popup = null;

                return true;
            }

            return false;
        }

        private void OnClicked(object sender, Controls.PopupResultEventArgs args)
        {
            RemovePopup();

            if (_command != null)
                _command.Execute(args);
        }
    }
}