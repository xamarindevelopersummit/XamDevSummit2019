using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamDevSummit.Controls
{
    public enum PopupResult
    {
        Ok,
        Cancel,
        Yes,
        No
    }

    public class PopupResultEventArgs : EventArgs
    {
        public PopupResult Result { get; set; }
    }

    public delegate void PopupResultEventHandler(object sender, PopupResultEventArgs e);

    public partial class BasePopup : ContentView
    {
        public IList<View> BasePopupContent => PopupContent.Children;

        public event PopupResultEventHandler Click;

        public BasePopup()
        {
            InitializeComponent();
        }

        protected void OnOkClicked(object sender, System.EventArgs e)
        {
            Click?.Invoke(this, new PopupResultEventArgs() { Result = PopupResult.Ok });
        }
    }
}