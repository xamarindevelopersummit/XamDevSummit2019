using Xamarin.Forms;

namespace XamDevSummit.Controls
{
    public partial class SelectionPopup : BasePopup
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(SelectionPopup), default(string), propertyChanged: OnTitlePropertyChanged);

        public static readonly BindableProperty MessageProperty =
            BindableProperty.Create(nameof(Message), typeof(string), typeof(SelectionPopup), default(string), propertyChanged: OnMessagePropertyChanged);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        private static void OnTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SelectionPopup me = (SelectionPopup)bindable;
            me.TitleLabel.Text = (string)newValue;
        }

        private static void OnMessagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SelectionPopup me = (SelectionPopup)bindable;
            me.MessageLabel.Text = (string)newValue;
        }

        public SelectionPopup()
        {
            InitializeComponent();
        }
    }
}