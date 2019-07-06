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

        private DelegateCommand _hamburgerCommand;
        public DelegateCommand HamburgerCommand
        {
            get => _hamburgerCommand ?? (_hamburgerCommand = new DelegateCommand(() => EventAggregator.GetEvent<HamburgerMenuEvent>().Publish()));
        }

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand
        {
            get => _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(async () => await NavigationService.GoBackAsync()));
        }

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