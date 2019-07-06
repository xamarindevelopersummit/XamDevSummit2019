using XamDevSummit.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using XamDevSummit.ViewModels;
using XamDevSummit.Services.Interfaces;
using XamDevSummit.Services;

namespace XamDevSummit
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var result = await NavigationService.NavigateAsync("MasterDetailShellPage/NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IPopupService, PopupService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MasterDetailShellPage, MasterDetailShellViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage>();
            containerRegistry.RegisterForNavigation<BasePage>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<NonModalSubPage, NonModalSubViewModel>();
            containerRegistry.RegisterForNavigation<ModalSubPage, ModalSubViewModel>();
        }
    }
}