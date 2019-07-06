using System.Collections.Generic;
using Xamarin.Forms;
using XamDevSummit.Models.Interfaces;

namespace XamDevSummit.Views
{
    public partial class BasePage : ContentPage
    {
        public IList<View> BasePageContent => BaseContentGrid.Children;

        #region Bindable properties
        public static readonly BindableProperty BasePageTitleProperty = BindableProperty.Create(nameof(BasePageTitle), typeof(string), typeof(BasePage), string.Empty, defaultBindingMode: BindingMode.OneWay, propertyChanged: BasePageTitleChanged);

        public string BasePageTitle
        {
            get => (string)GetValue(BasePageTitleProperty);
            set => SetValue(BasePageTitleProperty, value);
        }

        private static void BasePageTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && bindable is BasePage basePage)
                basePage.TitleLabel.Text = (string)newValue;
        }
        #endregion

        public BasePage()
        {
            InitializeComponent();

            //Hide the Xamarin Forms build in navigation header
            NavigationPage.SetHasNavigationBar(this, false);

            //Fix top page marging requirement depending on the current device running the app
            StatusRowDefinition.Height = DependencyService.Get<IDeviceInfo>().StatusbarHeight;
        }
    }
}