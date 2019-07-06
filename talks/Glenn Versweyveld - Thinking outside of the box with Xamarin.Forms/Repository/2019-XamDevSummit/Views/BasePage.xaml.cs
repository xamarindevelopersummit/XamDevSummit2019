using System.Collections.Generic;
using Prism.Events;
using Prism.Unity;
using Unity;
using Xamarin.Forms;
using XamDevSummit.Events;
using XamDevSummit.Models;
using XamDevSummit.Models.Interfaces;
using XamDevSummit.ViewModels;

namespace XamDevSummit.Views
{
    public partial class BasePage : ContentPage
    {
        public IList<View> BasePageContent => BaseContentGrid.Children;

        protected IUnityContainer Container { get; }
        protected IEventAggregator EventAggregator { get; }

        #region Bindable properties
        public static readonly BindableProperty BasePageTitleProperty = BindableProperty.Create(nameof(BasePageTitle), typeof(string), typeof(BasePage), string.Empty, defaultBindingMode: BindingMode.OneWay, propertyChanged: OnBasePageTitleChanged);

        public static readonly BindableProperty PageModeProperty = BindableProperty.Create(nameof(PageMode), typeof(PageMode), typeof(BasePage), PageMode.None, propertyChanged: OnPageModePropertyChanged);

        public string BasePageTitle
        {
            get => (string)GetValue(BasePageTitleProperty);
            set => SetValue(BasePageTitleProperty, value);
        }

        public PageMode PageMode
        {
            get => (PageMode)GetValue(PageModeProperty);
            set => SetValue(PageModeProperty, value);
        }

        private static void OnBasePageTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && bindable is BasePage basePage)
                basePage.TitleLabel.Text = (string)newValue;
        }

        private static void OnPageModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && bindable is BasePage basePage)
                basePage.SetPageMode((PageMode)newValue);
        }
        #endregion

        public BasePage()
        {
            InitializeComponent();

            //Hide the Xamarin Forms build in navigation header
            NavigationPage.SetHasNavigationBar(this, false);

            //Initialize the page mode
            SetPageMode(PageMode.None);

            //Fix top page marging requirement depending on the current device running the app
            StatusRowDefinition.Height = DependencyService.Get<IDeviceInfo>().StatusbarHeight;

            Container = ((PrismApplication)Application.Current).Container.GetContainer();
            EventAggregator = Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<HamburgerMenuEvent>().Subscribe(HandleHamburgerMenu);
        }

        private void HandleHamburgerMenu()
        {
            ((MasterDetailPage)Application.Current.MainPage).IsPresented = true;
        }

        private void HandleHamburgerMenuGesture(bool enable)
        {
            if (Application.Current.MainPage is MasterDetailPage)
                ((MasterDetailPage)Application.Current.MainPage).IsGestureEnabled = enable;
        }

        private void SetPageMode(PageMode pageMode)
        {
            if (BindingContext != null)
            {
                ((ViewModelBase)BindingContext).PageMode = pageMode;

                switch (pageMode)
                {
                    case PageMode.Menu:
                        HamburgerButton.IsVisible = true;
                        NavigateBackButton.IsVisible = false;
                        break;
                    case PageMode.Navigate:
                        HamburgerButton.IsVisible = false;
                        NavigateBackButton.IsVisible = true;
                        break;
                    default:
                        HamburgerButton.IsVisible = false;
                        NavigateBackButton.IsVisible = false;
                        break;
                }

                HandleHamburgerMenuGesture(PageMode == PageMode.Menu);
            }
        }
    }
}