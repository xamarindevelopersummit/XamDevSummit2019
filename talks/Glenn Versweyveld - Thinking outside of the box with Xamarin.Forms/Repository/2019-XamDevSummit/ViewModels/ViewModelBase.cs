using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using XamDevSummit.Events;
using XamDevSummit.Models;

namespace XamDevSummit.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        protected IEventAggregator EventAggregator { get; }

        public INavigationService NavigationService { get; }

        private PageMode _pageMode;
        public PageMode PageMode
        {
            get => _pageMode;
            set => SetProperty(ref _pageMode, value);
        }

        private bool _isFabButtonVisible = false;
        public bool IsFabButtonVisible
        {
            get => _isFabButtonVisible;
            set => SetProperty(ref _isFabButtonVisible, value);
        }

        private DelegateCommand _hamburgerCommand;
        public DelegateCommand HamburgerCommand => _hamburgerCommand ?? (_hamburgerCommand = new DelegateCommand(() => EventAggregator.GetEvent<HamburgerMenuEvent>().Publish()));

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand => _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(async () => await NavigationService.GoBackAsync()));

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(async () => await NavigationService.GoBackAsync(useModalNavigation: PageMode == PageMode.Modal)));

        public ViewModelBase(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            NavigationService = navigationService;
            EventAggregator = eventAggregator;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }
    }
}