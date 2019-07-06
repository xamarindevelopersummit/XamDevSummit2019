using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using XamDevSummit.Events;

namespace XamDevSummit.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        protected IEventAggregator EventAggregator { get; }

        public INavigationService NavigationService { get; }

        private DelegateCommand _hamburgerCommand;
        public DelegateCommand HamburgerCommand
        {
            get => _hamburgerCommand ?? (_hamburgerCommand = new DelegateCommand(() => EventAggregator.GetEvent<HamburgerMenuEvent>().Publish()));
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